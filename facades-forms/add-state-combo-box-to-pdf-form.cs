using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FieldType
using Aspose.Pdf;           // For completeness if needed

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // PDF containing a form
        const string outputPdf = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Define the rectangle for the combo box (coordinates in points)
        float llx = 100f; // lower‑left X
        float lly = 600f; // lower‑left Y
        float urx = 200f; // upper‑right X
        float ury = 620f; // upper‑right Y

        // Add a combo box field named "State" on page 1
        formEditor.AddField(FieldType.ComboBox, "State", 1, llx, lly, urx, ury);

        // List of US state abbreviations
        string[] states = new string[]
        {
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };

        // Populate the combo box with the state abbreviations
        foreach (string state in states)
        {
            formEditor.AddListItem("State", state);
        }

        // Persist the changes to the output PDF
        formEditor.Save();

        Console.WriteLine($"Combo box 'State' added and saved to '{outputPdf}'.");
    }
}