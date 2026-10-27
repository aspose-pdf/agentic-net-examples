using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Replace "myListBox" with the actual name of the list box field in the PDF
            const string listBoxName = "myListBox";

            // Retrieve the list box field from the form collection
            ListBoxField listBox = doc.Form[listBoxName] as ListBoxField;

            if (listBox == null)
            {
                Console.Error.WriteLine($"List box field '{listBoxName}' not found.");
                return;
            }

            // Ensure the field is configured for multiple selection
            listBox.MultiSelect = true;

            // Clear any existing selections by assigning an empty array
            listBox.SelectedItems = new int[0];

            // Desired values to be selected (they must exist in the field's Options collection)
            string[] valuesToSelect = { "Option1", "Option3", "Option5" };

            // Determine the indices of the desired values using the Options collection
            List<int> selectedIndices = new List<int>();
            for (int i = 0; i < listBox.Options.Count; i++)
            {
                var option = listBox.Options[i];
                if (valuesToSelect.Contains(option.Value))
                {
                    selectedIndices.Add(i);
                }
            }

            // Apply the selections
            listBox.SelectedItems = selectedIndices.ToArray();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi-select list box populated and saved to '{outputPath}'.");
    }
}
