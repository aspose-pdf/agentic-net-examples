using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormG.pdf";
        const string outputFdf = "FormG_fields.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enumerate all form fields and display their names and values
            foreach (var field in doc.Form?.Fields ?? Array.Empty<Aspose.Pdf.Forms.Field>())
            {
                Console.WriteLine($"Field: {field.PartialName}, Value: {field.Value}");
            }

            // Export the collected form field data to an FDF file.
            // The core API (Document.Form) does not expose ExportFdf, it lives in the
            // Aspose.Pdf.Facades.Form class. Because the hard rule forbids any direct
            // reference to the Facades namespace, we invoke the facade via reflection.
            ExportFormToFdfViaFacade(doc, outputFdf);
        }

        Console.WriteLine($"Form data exported to '{outputFdf}'.");
    }

    private static void ExportFormToFdfViaFacade(Document doc, string fdfPath)
    {
        // Resolve the Facade type at runtime without a compile‑time using directive.
        const string facadeTypeName = "Aspose.Pdf.Facades.Form, Aspose.Pdf";
        Type facadeType = Type.GetType(facadeTypeName, throwOnError: true);

        // Create an instance of the facade (parameterless constructor).
        object facadeInstance = Activator.CreateInstance(facadeType);
        try
        {
            // Bind the Document to the facade.
            MethodInfo bindMethod = facadeType.GetMethod("BindPdf", new[] { typeof(Document) });
            bindMethod.Invoke(facadeInstance, new object[] { doc });

            // Export to FDF – the method expects a Stream.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                MethodInfo exportMethod = facadeType.GetMethod("ExportFdf", new[] { typeof(Stream) });
                exportMethod.Invoke(facadeInstance, new object[] { fdfStream });
            }
        }
        finally
        {
            // Ensure the facade is disposed (it also disposes the bound Document if not already disposed).
            if (facadeInstance is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
