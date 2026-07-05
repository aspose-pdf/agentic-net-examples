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
        const string listBoxName = "myListBox"; // name of the ListBox field in the PDF
        const int desiredIndex = 2; // index of the option to select (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form != null)
            {
                // Retrieve the field by name using the indexer; returns null if not found
                ListBoxField listBox = doc.Form[listBoxName] as ListBoxField;
                if (listBox != null)
                {
                    // Verify the desired index is within the range of available options
                    int optionsCount = listBox.Options.Count; // Options collection is zero‑based
                    if (desiredIndex >= 1 && desiredIndex <= optionsCount)
                    {
                        // ListBoxField.Selected is zero‑based, so subtract 1 from the 1‑based index
                        listBox.Selected = desiredIndex - 1;
                    }
                    else
                    {
                        Console.Error.WriteLine($"Desired index {desiredIndex} is out of range (1‑{optionsCount}).");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"ListBox field '{listBoxName}' not found or is not a ListBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
