using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Rectangle buttonRect = new Rectangle(100, 700, 200, 750);

        // Create a button field (Aspose.Pdf.Forms.ButtonField)
        ButtonField printButton = new ButtonField(doc.Pages[1], buttonRect)
        {
            PartialName = "PrintForm",
            Value = "Print"
        };

        // JavaScript that opens the print dialog when the button is pressed
        string js = "this.print({bUI:true,bSilent:false,bShrinkToFit:true});";

        // Assign the JavaScript to the button's mouse‑press action (valid property)
        printButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

        // Add the button to the form on page 1 (page numbers are 1‑based)
        doc.Form.Add(printButton, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with PrintForm button: {outputPath}");
    }
}