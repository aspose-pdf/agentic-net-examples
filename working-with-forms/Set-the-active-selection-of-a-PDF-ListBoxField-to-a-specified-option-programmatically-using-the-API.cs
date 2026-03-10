using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF containing a ListBox field
        const string outputPath = "output.pdf";         // Resulting PDF with selection set
        const string listBoxName = "MyListBox";         // Exact name of the ListBox field
        const int selectedIndex = 2;                    // Desired option (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field from the form collection
            // The Form collection returns a generic Field; cast to ListBoxField
            if (doc.Form[listBoxName] is ListBoxField listBox)
            {
                // Set the selected option (items are numbered from 1)
                listBox.Selected = selectedIndex;

                // Alternatively, for multiselect ListBox you could assign an array:
                // listBox.SelectedItems = new int[] { selectedIndex };
            }
            else
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found.");
                return;
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"ListBox selection set and saved to '{outputPath}'.");
    }
}