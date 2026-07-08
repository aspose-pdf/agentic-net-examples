using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object of the document
            Aspose.Pdf.Forms.Form pdfForm = doc.Form;

            // Define the rectangle where the progress bar field will be placed
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 550, 770);

            // Create a read‑only text box field that will act as the progress bar
            TextBoxField progressField = new TextBoxField(doc, progressRect);
            progressField.PartialName = "ProgressBar";               // field name
            progressField.Color = Aspose.Pdf.Color.LightGray;        // border color
            progressField.ReadOnly = true;                           // user cannot edit
            progressField.Value = "0%";                              // initial value

            // Add the field to the document's form
            pdfForm.Add(progressField);

            // Simulate completion of sections and update the progress bar
            UpdateProgress(progressField, 33);   // after first section
            UpdateProgress(progressField, 66);   // after second section
            UpdateProgress(progressField, 100);  // after all sections completed

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }

    // Helper method to update the progress bar field value
    static void UpdateProgress(TextBoxField field, int percent)
    {
        // Ensure percent is within 0‑100 range
        if (percent < 0) percent = 0;
        if (percent > 100) percent = 100;

        field.Value = percent.ToString() + "%";

        // Optionally change the border color to give visual feedback
        if (percent == 100)
            field.Color = Aspose.Pdf.Color.Green;
        else if (percent >= 50)
            field.Color = Aspose.Pdf.Color.Orange;
        else
            field.Color = Aspose.Pdf.Color.Red;
    }
}