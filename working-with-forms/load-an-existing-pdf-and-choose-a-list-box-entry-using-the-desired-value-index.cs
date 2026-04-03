using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing a ListBox field
        const string inputPath = "input.pdf";
        // Output PDF after selecting the desired list box entry
        const string outputPath = "output.pdf";
        // Name of the ListBox field in the PDF (as defined in the form)
        const string listBoxName = "MyListBox";
        // Desired index to select (items are 1‑based; 1 = first item)
        int desiredIndex = 2; // example: select the second item

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field by its name from the form collection.
            // Document.Form works like a dictionary but does not expose ContainsKey.
            // Access the field directly and verify the cast.
            if (doc.Form[listBoxName] is ListBoxField listBox)
            {
                // Ensure the desired index is within the valid range.
                // ListBoxField.Options.Count gives the number of items.
                if (desiredIndex >= 1 && desiredIndex <= listBox.Options.Count)
                {
                    // ListBoxField.Selected is zero‑based, so subtract 1.
                    listBox.Selected = desiredIndex - 1;
                    Console.WriteLine($"ListBox '{listBoxName}' selection set to index {desiredIndex}.");
                }
                else
                {
                    Console.Error.WriteLine($"Desired index {desiredIndex} is out of range. Options count: {listBox.Options.Count}");
                }
            }
            else
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
