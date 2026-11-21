using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string listBoxName = "myListBox";   // name of the ListBox field in the PDF
        const int desiredIndex = 2;               // zero‑based index of the entry to select

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field by name
            ListBoxField listBox = doc.Form[listBoxName] as ListBoxField;
            if (listBox == null)
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found.");
            }
            else
            {
                // ListBoxField uses 1‑based indices for selection via SelectedItems
                int oneBasedIndex = desiredIndex + 1;
                listBox.SelectedItems = new[] { oneBasedIndex };
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ListBox selection to '{outputPath}'.");
    }
}
