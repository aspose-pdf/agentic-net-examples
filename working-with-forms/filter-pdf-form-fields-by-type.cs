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
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all form fields
            Field[] allFields = doc.Form.Fields;

            // LINQ query to get only TextBoxField instances
            var textBoxFields = allFields.OfType<TextBoxField>();

            foreach (var tb in textBoxFields)
            {
                Console.WriteLine($"TextBox Field - Name: {tb.Name}, Value: {tb.Value}");
            }

            // Example: LINQ query to get only ListBoxField instances
            var listBoxFields = allFields.OfType<ListBoxField>();

            foreach (var lb in listBoxFields)
            {
                Console.WriteLine($"ListBox Field - Name: {lb.Name}");
            }

            // If modifications were made, you could save the document here
            // doc.Save("output.pdf");
        }
    }
}