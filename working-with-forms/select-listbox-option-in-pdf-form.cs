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
        const int desiredIndex = 2; // 1‑based index of the option to select

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the ListBox field by its name (replace "MyListBox" with the actual field name)
            ListBoxField listBox = doc.Form["MyListBox"] as ListBoxField;
            if (listBox == null)
            {
                Console.Error.WriteLine("ListBox field not found.");
                return;
            }

            // Validate the desired index against the number of options
            if (desiredIndex < 1 || desiredIndex > listBox.Options.Count)
            {
                Console.Error.WriteLine("Desired index is out of range.");
                return;
            }

            // Set the selected option (items are numbered from 1)
            listBox.Selected = desiredIndex;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"ListBox selection saved to '{outputPath}'.");
    }
}