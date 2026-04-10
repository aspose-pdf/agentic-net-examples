using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Fill the "Price" field with a value formatted to 2 decimals
        // -----------------------------------------------------------------
        decimal priceValue = 123.456m; // example value; replace as needed
        string formattedPrice = priceValue.ToString("F2", CultureInfo.InvariantCulture);

        using (Form form = new Form())
        {
            form.BindPdf(inputPdf);
            // Fill the field with the formatted string
            form.FillField("Price", formattedPrice);
            // Save the intermediate PDF (still without custom appearance)
            form.Save(outputPdf);
        }

        // ---------------------------------------------------------------
        // Step 2: Apply visual appearance to the "Price" field (e.g., alignment)
        // ---------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(outputPdf);

            // Configure visual attributes via FormFieldFacade
            editor.Facade = new FormFieldFacade
            {
                // Example: right‑align the text inside the field
                Alignment = FormFieldFacade.AlignRight,
                // You can also set colors, font, etc., if desired:
                // BackgroundColor = System.Drawing.Color.LightYellow,
                // TextColor       = System.Drawing.Color.DarkBlue,
                // FontSize        = 12
            };

            // Apply the facade settings to the specific field
            editor.DecorateField("Price");

            // Persist the changes
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}