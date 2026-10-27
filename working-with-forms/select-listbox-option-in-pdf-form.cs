using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // existing PDF with a list box
        const string outputPath = "output.pdf";        // PDF after selection
        const string listBoxName = "myListBox";        // name of the ListBox field
        const int desiredIndex = 2;                     // zero‑based index of the entry to select

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field by its name using the Fields collection
            var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == listBoxName);
            if (field == null)
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found.");
                return;
            }

            if (!(field is ListBoxField listBox))
            {
                Console.Error.WriteLine($"Field '{listBoxName}' is not a ListBox.");
                return;
            }

            // Ensure the desired index is within the Options collection bounds
            if (desiredIndex < 0 || desiredIndex >= listBox.Options.Count)
            {
                Console.Error.WriteLine($"Index {desiredIndex} is out of range. Options count: {listBox.Options.Count}");
                return;
            }

            // Set the selected index (ListBoxField.SelectedItems expects an int[] of indexes)
            listBox.SelectedItems = new int[] { desiredIndex };

            // Save the modified PDF (still PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"ListBox entry at index {desiredIndex} selected and saved to '{outputPath}'.");
    }
}
