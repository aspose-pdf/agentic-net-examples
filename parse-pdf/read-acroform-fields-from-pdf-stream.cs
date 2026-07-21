using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a stream and create a Document from it
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (Document doc = new Document(pdfStream))
        {
            // Check if the document contains AcroForm fields
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No AcroForm fields found in the PDF.");
                return;
            }

            Console.WriteLine("AcroForm fields:");
            // Iterate over each field and output its name and value
            foreach (Field field in doc.Form.Fields)
            {
                string value = field.Value?.ToString() ?? "(null)";
                Console.WriteLine($"- {field.FullName}: {value}");
            }
        }
    }
}
