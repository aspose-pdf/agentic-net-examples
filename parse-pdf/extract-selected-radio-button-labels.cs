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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only radio button fields
                if (field is RadioButtonField radioField)
                {
                    // The Options collection contains Option objects (Name/Value pairs)
                    var options = radioField.Options;

                    // Selected is 1‑based; 0 means no selection
                    int selectedIndex = radioField.Selected;

                    // Retrieve the display label (Value) of the selected option
                    string selectedLabel = selectedIndex > 0 && selectedIndex <= options.Count
                        ? options[selectedIndex - 1].Value   // Option.Value holds the visible label
                        : "(none)";

                    Console.WriteLine($"Radio Button '{radioField.FullName}': Selected = {selectedLabel}");
                }
            }
        }
    }
}
