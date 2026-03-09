using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string editedPath = "output_modified.pdf";
        const string filledPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and edit its form fields
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor(pdfDoc))
            {
                // Add a new text field on page 1
                formEditor.AddField(
                    Aspose.Pdf.Facades.FieldType.Text,
                    "SampleText",
                    1,
                    100f, 500f, 300f, 520f);

                // Make the field read‑only
                formEditor.SetFieldAttribute("SampleText", Aspose.Pdf.Facades.PropertyFlag.ReadOnly);

                // Limit the field to 20 characters
                formEditor.SetFieldLimit("SampleText", 20);

                // Configure visual appearance via FormFieldFacade
                Aspose.Pdf.Facades.FormFieldFacade facade = new Aspose.Pdf.Facades.FormFieldFacade
                {
                    BackgroundColor = System.Drawing.Color.LightGray,
                    TextColor = System.Drawing.Color.DarkBlue,
                    BorderColor = System.Drawing.Color.Black,
                    BorderWidth = Aspose.Pdf.Facades.FormFieldFacade.BorderWidthThin,
                    Alignment = Aspose.Pdf.Facades.FormFieldFacade.AlignCenter
                };
                formEditor.Facade = facade;

                // Apply the visual settings to the newly added field
                formEditor.DecorateField("SampleText");

                // Save the edited PDF
                formEditor.Save(editedPath);
            }
        }

        // Fill the field using the Facades Form class
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(editedPath))
        {
            // Fill the text field with a value
            form.FillField("SampleText", "Hello World");

            // Retrieve appearance information (example)
            Aspose.Pdf.Facades.FormFieldFacade fieldFacade = form.GetFieldFacade("SampleText");
            Console.WriteLine($"Border color after fill: {fieldFacade.BorderColor}");
            Console.WriteLine($"Background color after fill: {fieldFacade.BackgroundColor}");

            // Save the filled PDF
            form.Save(filledPath);
        }

        Console.WriteLine($"Edited PDF saved to: {editedPath}");
        Console.WriteLine($"Filled PDF saved to: {filledPath}");
    }
}
