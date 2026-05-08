using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be read.
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF as a read‑only stream.
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        {
            // Load the PDF document from the stream.
            using (Document doc = new Document(pdfStream))
            {
                // Check whether the document contains an AcroForm.
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No AcroForm fields found in the document.");
                    return;
                }

                // Enumerate all form fields and output their names and values.
                foreach (Field field in doc.Form)
                {
                    string fieldName = field?.FullName ?? "(unnamed)";
                    string fieldValue = field?.Value?.ToString() ?? "(null)";
                    Console.WriteLine($"Field: {fieldName}  Value: {fieldValue}");
                }
            }
        }
    }
}