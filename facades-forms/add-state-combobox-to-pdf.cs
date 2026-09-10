using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FieldType
using Aspose.Pdf;          // Document (if needed)

// Ensure Aspose.Pdf license is set if required
// Aspose.Pdf.License license = new Aspose.Pdf.License();
// license.SetLicense("Aspose.Pdf.lic");

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // existing PDF to add the combo box to
        const string outputPdfPath = "output_with_state_combobox.pdf";

        // Verify the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // List of US state abbreviations
        string[] usStates = new string[]
        {
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };

        // Use FormEditor to bind the existing PDF, add a combo box field, populate it, and save.
        // FormEditor implements SaveableFacade, so we can use a using block for deterministic disposal.
        using (FormEditor formEditor = new FormEditor(inputPdfPath, outputPdfPath))
        {
            // Add a combo box field named "State" on page 1.
            // Parameters: field type, field name, page number (1‑based), lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // Adjust the rectangle coordinates as needed for your layout.
            formEditor.AddField(FieldType.ComboBox, "State", 1, 100f, 700f, 200f, 720f);

            // Populate the combo box with the state abbreviations.
            foreach (string state in usStates)
            {
                formEditor.AddListItem("State", state);
            }

            // Save the modified PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Combo box \"State\" added and populated. Output saved to '{outputPdfPath}'.");
    }
}