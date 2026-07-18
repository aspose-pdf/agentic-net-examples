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
        const string fieldName = "ListBox1";   // replace with the actual field name
        const int selectedIndex = 2;           // items are 1‑based

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field by its name
            ListBoxField listBox = doc.Form[fieldName] as ListBoxField;
            if (listBox == null)
            {
                Console.Error.WriteLine($"ListBox field '{fieldName}' not found.");
                return;
            }

            // Set the selected item (index starts at 1)
            listBox.Selected = selectedIndex;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"ListBox selection saved to '{outputPath}'.");
    }
}