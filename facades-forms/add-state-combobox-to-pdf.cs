using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor and bind the existing PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Define the rectangle for the combo box (llx, lly, urx, ury)
            // Add a combo box named "State" on page 1 (no default value)
            formEditor.AddField(FieldType.ComboBox, "State", 1, 100F, 700F, 250F, 730F);

            // US state abbreviations
            string[] states = new string[]
            {
                "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
            };

            // Populate the combo box with the state abbreviations
            foreach (string state in states)
            {
                formEditor.AddListItem("State", state);
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Combo box 'State' added and saved to '{outputPath}'.");
    }
}
