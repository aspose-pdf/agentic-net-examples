using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
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

        // Initialize FormEditor and bind the PDF document
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Access the underlying Document object
        Document doc = formEditor.Document;

        // Iterate through all pages and annotations to find the "Discount" field
        foreach (Page page in doc.Pages)
        {
            foreach (Annotation ann in page.Annotations)
            {
                // In Aspose.PDF the numeric field is represented by TextBoxField.
                if (ann is TextBoxField textBox && textBox.Name == "Discount")
                {
                    // Limit the total number of characters (e.g., 10 characters)
                    textBox.MaxLen = 10;

                    // Set the field to be a comb field with enough combs for two decimals
                    // (e.g., 7 combs for format 99999.99)
                    formEditor.SetFieldCombNumber(textBox.Name, 7);

                    // Add a JavaScript action that forces numeric input with two‑decimal precision.
                    // The script runs when the field value is calculated (e.g., on blur).
                    string js = @"var v = parseFloat(event.value);
if (!isNaN(v)) {
    event.value = v.toFixed(2);
} else {
    app.alert('Please enter a numeric value');
    event.rc = false; // reject the change
}";
                    textBox.Actions.OnCalculate = new JavascriptAction(js);
                }
            }
        }

        // Save the modified PDF using FormEditor (inherits SaveableFacade)
        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
