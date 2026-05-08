using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF with a ListBox field
        const string outputPath = "output.pdf";         // PDF after selection is set
        const string listBoxName = "MyListBox";         // name of the ListBox field in the PDF
        const int desiredIndex = 2;                     // index of the option to select (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field by its name from the form collection
            // The Form collection returns a generic Field; cast it to ListBoxField
            if (doc.Form[listBoxName] is ListBoxField listBox)
            {
                // Set the selected option index (items are numbered from 1)
                listBox.Selected = desiredIndex;
                Console.WriteLine($"ListBox '{listBoxName}' selection set to index {desiredIndex}.");
            }
            else
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}