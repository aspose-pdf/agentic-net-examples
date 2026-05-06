using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for DefaultAppearance class
using Aspose.Pdf.Forms;         // for Form handling
using Aspose.Pdf.Text;          // for Aspose.Pdf.Color

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form (otherwise nothing to set)
            if (doc.Form != null)
            {
                // Set the default appearance for the entire form:
                // Font: Helvetica, Size: 12 points, Color: Blue
                doc.Form.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);
            }

            // Save the modified PDF (using rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated field default appearance to '{outputPath}'.");
    }
}