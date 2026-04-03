using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // for FormEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the reset button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the button rectangle (left, bottom, right, top)
            // Note: Aspose.Pdf.Rectangle constructor expects (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field
            ButtonField resetButton = new ButtonField(page, buttonRect)
            {
                // Set the visible caption of the button
                NormalCaption = "Reset",
                // Optional: set a background color for visibility
                Color = Aspose.Pdf.Color.LightGray,
                // Give the button a unique name so we can attach a script later
                Name = "ResetBtn"
            };

            // Add the button to the document's form collection
            doc.Form.Add(resetButton);

            // Save the PDF first – the FormEditor works with a file path
            doc.Save(outputPath);
        }

        // Attach JavaScript that resets the whole form using FormEditor
        FormEditor editor = new FormEditor();
        editor.BindPdf(outputPath);
        // JavaScript to clear all fields
        string js = "this.resetForm();";
        editor.SetFieldScript("ResetBtn", js);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
