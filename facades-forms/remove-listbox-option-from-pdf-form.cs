using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "contract_template.pdf";
        const string outputPath = "cleaned_template.pdf";
        const string fieldName = "Options";
        const string itemToRemove = "OldOption";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the ListBox field via the document's Form collection
            if (doc.Form.HasField(fieldName) && doc.Form[fieldName] is ListBoxField listBox)
            {
                // Remove the specified option if it exists
                listBox.DeleteOption(itemToRemove);
            }
            else
            {
                Console.Error.WriteLine($"ListBox field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
