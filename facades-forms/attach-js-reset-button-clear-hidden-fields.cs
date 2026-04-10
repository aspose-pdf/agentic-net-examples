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
        const string resetButtonName = "ResetForm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade and bind the document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // JavaScript that resets the form and clears hidden fields
                string js = @"
resetForm();
var hidden1 = this.getField('HiddenField1');
if (hidden1 != null) hidden1.value = '';
var hidden2 = this.getField('HiddenField2');
if (hidden2 != null) hidden2.value = '';
";

                // Attach the script to the reset button field
                formEditor.AddFieldScript(resetButtonName, js);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }
}