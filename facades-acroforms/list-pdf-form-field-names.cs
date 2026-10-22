using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Retrieve all form fields from the document's Form collection
        var fields = doc.Form?.Fields;

        Console.WriteLine("Form field names:");
        if (fields != null && fields.Count() > 0)
        {
            foreach (var field in fields)
            {
                // The field name is stored in the PartialName property
                Console.WriteLine(field.PartialName);
            }
        }
        else
        {
            Console.WriteLine("No form fields found.");
        }

        // No modifications are made, so no need to save the document.
        // The Document instance will be released when the program ends.
    }
}