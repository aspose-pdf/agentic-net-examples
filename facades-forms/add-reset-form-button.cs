using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        using (Document doc = new Document(inputPath))
        {
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Define button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100f, 100f, 200f, 130f);

                // Add a push button field named "ResetForm" on page 1
                // The AddField overload expects float values for the rectangle coordinates.
                formEditor.AddField(
                    FieldType.PushButton,
                    "ResetForm",
                    1,
                    (float)buttonRect.LLX,
                    (float)buttonRect.LLY,
                    (float)buttonRect.URX,
                    (float)buttonRect.URY);

                // Attach JavaScript that clears all form fields when the button is clicked
                formEditor.AddFieldScript("ResetForm", "this.resetForm();");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}
