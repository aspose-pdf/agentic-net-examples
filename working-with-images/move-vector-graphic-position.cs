using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "moved_vector.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work on the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Retrieve the XObject collection. The property name changed between
            // Aspose.Pdf versions ("XObjects" in older releases, "XObjectResources"
            // in newer ones). We use reflection to support both without a compile‑time
            // dependency on a specific version.
            var resources = page.Resources;
            var xObjectProp = resources.GetType().GetProperty("XObjects") ??
                               resources.GetType().GetProperty("XObjectResources");
            if (xObjectProp == null)
            {
                Console.Error.WriteLine("No XObject collection found on the page resources.");
                doc.Save(outputPath);
                return;
            }

            var xObjectCollection = xObjectProp.GetValue(resources) as System.Collections.IEnumerable;
            if (xObjectCollection == null)
            {
                Console.Error.WriteLine("Unable to enumerate XObjects.");
                doc.Save(outputPath);
                return;
            }

            // Define translation offsets (e.g., 50 points right, 30 points up)
            const double dx = 50;   // horizontal shift
            const double dy = -30;  // vertical shift (PDF origin is bottom‑left)

            // Use reflection to manipulate FormXObject objects without referencing
            // the Aspose.Pdf.XObjects namespace (which may be absent in some versions).
            foreach (var obj in xObjectCollection)
            {
                if (obj == null) continue;

                var objType = obj.GetType();
                // FormXObject (or any XObject that supports a Matrix) has a "Matrix" property.
                var matrixProp = objType.GetProperty("Matrix");
                if (matrixProp == null) continue;

                // Get or create the Matrix instance.
                var matrix = matrixProp.GetValue(obj);
                if (matrix == null)
                {
                    // Matrix class lives in Aspose.Pdf namespace.
                    var matrixCtor = typeof(Matrix).GetConstructor(Type.EmptyTypes);
                    matrix = matrixCtor.Invoke(null);
                    matrixProp.SetValue(obj, matrix);
                }

                // Call Matrix.Translate(dx, dy) via reflection.
                var translateMethod = matrix.GetType().GetMethod("Translate", new[] { typeof(double), typeof(double) });
                if (translateMethod != null)
                {
                    translateMethod.Invoke(matrix, new object[] { dx, dy });
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphic(s) moved and saved to '{outputPath}'.");
    }
}
