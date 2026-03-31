using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "output.pdf";

        // Create a simple PDF with one blank page if it does not already exist
        if (!File.Exists(templatePath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                doc.Save(templatePath);
            }
        }

        // Initialize FormEditor with the source PDF and the destination PDF
        FormEditor formEditor = new FormEditor(templatePath, outputPath);

        // Add a combo box field named "State" on page 1
        // Parameters: field type, field name, default value, page number, llx, lly, urx, ury
        formEditor.AddField(FieldType.ComboBox, "State", "", 1, 100f, 500f, 200f, 520f);

        // US state abbreviations to populate the combo box
        string[] states = new string[]
        {
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };

        // Add each state abbreviation as an item in the combo box
        foreach (string state in states)
        {
            formEditor.AddListItem("State", state);
        }

        // Save the updated PDF with the combo box field
        formEditor.Save();
    }
}
