using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for WidgetAnnotation

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Iterate through all fields in the form
            foreach (WidgetAnnotation field in form)
            {
                // Process only radio button fields
                if (field is RadioButtonField radioButton)
                {
                    // The Options collection holds the display labels for each option
                    var options = radioButton.Options;

                    // Selected is 1‑based; 0 means no option is selected
                    int selectedIndex = radioButton.Selected;

                    if (selectedIndex > 0 && selectedIndex <= options.Count)
                    {
                        // Retrieve the label (Name) of the selected option
                        string selectedLabel = options[selectedIndex - 1].Name;

                        // The Value property of the option holds the export value
                        string exportValue = options[selectedIndex - 1].Value;

                        Console.WriteLine($"Radio Button '{radioButton.FullName}':");
                        Console.WriteLine($"  Selected Option Label : {selectedLabel}");
                        Console.WriteLine($"  Export Value          : {exportValue}");
                    }
                    else
                    {
                        Console.WriteLine($"Radio Button '{radioButton.FullName}' has no selection.");
                    }
                }
            }
        }
    }
}
