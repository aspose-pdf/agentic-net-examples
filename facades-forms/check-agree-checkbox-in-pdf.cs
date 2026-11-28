using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Agreement.pdf";
        const string checkBoxName = "Agree"; // name of the checkbox field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Retrieve the checkbox field from the document's form collection
        var checkBox = doc.Form[checkBoxName] as CheckboxField;
        if (checkBox == null)
        {
            Console.Error.WriteLine($"Checkbox field '{checkBoxName}' not found.");
            return;
        }

        // Mark the checkbox as checked
        checkBox.Checked = true; // alternatively: checkBox.Value = "On";

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Checkbox '{checkBoxName}' checked and saved to '{outputPath}'.");
    }
}