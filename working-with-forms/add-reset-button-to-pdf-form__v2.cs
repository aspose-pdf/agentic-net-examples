using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Create a reset button on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            ButtonField resetButton = new ButtonField(page, rect);
            resetButton.Name = "ResetButton";
            resetButton.AlternateCaption = "Reset";

            // JavaScript to clear all form fields – use a valid action property
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the form
            form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with reset button: {outputPath}");
    }
}
