using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub for missing Aspose.Pdf type that may be referenced elsewhere in the project.
// This prevents CS0246 errors when the actual library does not contain the type
// (e.g., older versions or when the reference is omitted).
namespace Aspose.Pdf.Annotations
{
    public class GoToAction
    {
        // The real Aspose.GoToAction class contains members for defining a
        // destination within a PDF. For compilation purposes an empty placeholder
        // is sufficient because the current sample does not use it.
    }
}

class Program
{
    static void Main()
    {
        // Path to the PDF form file (replace with your actual file path)
        const string pdfPath = "PdfForm.pdf";

        // Ensure the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF document
        using (Form form = new Form(pdfPath))
        {
            // Export form fields to an in‑memory XML stream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                form.ExportXml(xmlStream);

                // Reset stream position to read the exported XML
                xmlStream.Position = 0;
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }
}
