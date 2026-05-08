using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "PaymentMethod.pdf";

        // Create a new PDF with three blank pages
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // Page 1
            doc.Pages.Add(); // Page 2
            doc.Pages.Add(); // Page 3

            // Initialize FormEditor and bind the document
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(doc);

            // Define the radio button options
            formEditor.Items = new[] { "Credit", "PayPal" };
            formEditor.RadioHoriz = true;   // arrange horizontally (default)
            formEditor.RadioGap = 10;       // optional gap between buttons

            // Add the radio button group on page 3
            // Rectangle coordinates: lower‑left (100, 500), upper‑right (200, 530)
            formEditor.AddField(FieldType.Radio, "PaymentMethod", 3, 100, 500, 200, 530);

            // Persist the changes
            doc.Save(outputPath);
            formEditor.Close(); // release facade resources
        }

        Console.WriteLine($"Radio button group 'PaymentMethod' added to '{outputPath}'.");
    }
}