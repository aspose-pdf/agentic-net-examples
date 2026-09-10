using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class ExportFormToFdf
{
    static void Main()
    {
        // Paths for the source PDF and the output FDF file
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(pdfPath))
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            // Load the Form facade via reflection (avoids direct reference to Aspose.Pdf.Facades namespace)
            Type formFacadeType = Type.GetType("Aspose.Pdf.Facades.Form, Aspose.Pdf");
            if (formFacadeType == null)
            {
                Console.Error.WriteLine("Unable to load Aspose.Pdf.Facades.Form type.");
                return;
            }

            object formFacade = Activator.CreateInstance(formFacadeType);

            // Bind the PDF document to the facade
            MethodInfo bindMethod = formFacadeType.GetMethod("BindPdf", new[] { typeof(Document) });
            bindMethod?.Invoke(formFacade, new object[] { doc });

            // Export the form data to the FDF stream
            MethodInfo exportMethod = formFacadeType.GetMethod("ExportFdf", new[] { typeof(Stream) });
            exportMethod?.Invoke(formFacade, new object[] { fdfStream });
        }

        Console.WriteLine($"Form data exported to FDF file: {fdfPath}");
    }
}
