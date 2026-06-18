using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a TextBoxField named "Price"
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Rectangle coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Rectangle rect = new Rectangle(100, 600, 200, 620);
            TextBoxField priceField = new TextBoxField(doc.Pages[1], rect);
            priceField.PartialName = "Price";
            doc.Form.Add(priceField, 1);
            doc.Save("input.pdf");
        }

        // Step 2: Open the PDF and set the field value formatted to two decimal places
        using (Document doc = new Document("input.pdf"))
        {
            TextBoxField priceField = (TextBoxField)doc.Form["Price"];
            double priceValue = 123.456;
            priceField.Value = priceValue.ToString("F2"); // two decimal places
            doc.Save("intermediate.pdf");
        }

        // Step 3: Use FormEditor to change the field appearance (e.g., make it printable)
        using (FormEditor editor = new FormEditor("intermediate.pdf", "output.pdf"))
        {
            editor.SetFieldAppearance("Price", AnnotationFlags.Print);
        }

        Console.WriteLine("PDF created with formatted Price field.");
    }
}
