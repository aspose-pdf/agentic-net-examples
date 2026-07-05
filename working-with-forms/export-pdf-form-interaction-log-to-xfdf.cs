using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF with a filled form
        const string xfdfPath = "interaction_log.xfdf"; // XFDF (XML) log file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // OPTIONAL: manipulate form fields here if needed, e.g.:
            // pdfDocument.Form["FieldName"].Value = "New Value";

            // Export all annotations (including form field data) to XFDF (XML) format
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Interaction log exported to '{xfdfPath}'.");
    }
}