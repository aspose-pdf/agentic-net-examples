using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only radio button fields
                if (field is RadioButtonField radioButton)
                {
                    // Selected index is 1‑based; 0 means no option is selected
                    int selectedIndex = radioButton.Selected;

                    if (selectedIndex > 0 && selectedIndex <= radioButton.Options.Count)
                    {
                        // Retrieve the label (option name) corresponding to the selected index
                        // Options collection holds Option objects; use the Name property for the label
                        string selectedLabel = radioButton.Options[selectedIndex - 1].Name;
                        Console.WriteLine($"Radio button '{radioButton.FullName}' selected option: '{selectedLabel}'");
                    }
                    else
                    {
                        Console.WriteLine($"Radio button '{radioButton.FullName}' has no selection.");
                    }
                }
            }
        }
    }
}
