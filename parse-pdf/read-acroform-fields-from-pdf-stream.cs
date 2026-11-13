using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

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

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        // Initialize the Aspose.Pdf Document from the stream
        using (Document doc = new Document(pdfStream))
        {
            // Ensure the document contains an AcroForm with fields
            if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Count() > 0)
            {
                Console.WriteLine($"AcroForm fields count: {doc.Form.Fields.Count()}");

                // Iterate over each field and output its name and value
                foreach (var field in doc.Form.Fields)
                {
                    // field is of type Aspose.Pdf.Form.Field
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
