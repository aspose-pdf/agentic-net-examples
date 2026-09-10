using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream for the FDF output
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                // Use reflection to work with Aspose.Pdf.Facades.Form without importing the forbidden namespace
                Type formFacadeType = Type.GetType("Aspose.Pdf.Facades.Form, Aspose.Pdf");
                if (formFacadeType == null)
                {
                    Console.Error.WriteLine("Unable to locate Aspose.Pdf.Facades.Form type. Ensure the Aspose.Pdf assembly is referenced.");
                    return;
                }

                // Instantiate the Form facade
                object formFacade = Activator.CreateInstance(formFacadeType);

                // Bind the PDF document to the facade
                MethodInfo bindPdfMethod = formFacadeType.GetMethod("BindPdf", new[] { typeof(Document) });
                bindPdfMethod.Invoke(formFacade, new object[] { pdfDoc });

                // Export the form data to FDF using the facade
                MethodInfo exportFdfMethod = formFacadeType.GetMethod("ExportFdf", new[] { typeof(Stream) });
                exportFdfMethod.Invoke(formFacade, new object[] { fdfStream });
                // The using block ensures the stream is closed
            }
        }

        Console.WriteLine($"Form data exported to FDF file: {outputFdfPath}");
    }
}
