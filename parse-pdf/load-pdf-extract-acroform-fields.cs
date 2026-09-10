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

        // Verify the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF into a Document instance.
        // The using statement ensures the Document is disposed properly.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Access the AcroForm associated with the document.
            var acroForm = pdfDoc.Form;

            // Form.Fields is a property (collection), not a method.
            var fields = acroForm?.Fields;

            if (fields != null && fields.Count() > 0)
            {
                Console.WriteLine($"AcroForm contains {fields.Count()} field(s).");

                // Iterate over each form field and output its name and current value.
                foreach (Field field in fields)
                {
                    Console.WriteLine($"Field: {field.FullName}, Value: {field.Value}");
                }
            }
            else
            {
                Console.WriteLine("The document does not contain an AcroForm or has no fields.");
            }
        }
    }
}
