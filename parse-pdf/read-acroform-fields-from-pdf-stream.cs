using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (replace with your actual file)
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        {
            // Load the PDF document from the stream
            using (Document doc = new Document(pdfStream))
            {
                // Ensure the document contains an AcroForm and that it has fields
                if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Count() > 0)
                {
                    // Iterate over all form fields and output their names and values
                    foreach (Field field in doc.Form.Fields)
                    {
                        Console.WriteLine($"Field Name: {field.FullName}, Value: {field.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("No AcroForm fields found in the document.");
                }
            }
        }
    }
}
