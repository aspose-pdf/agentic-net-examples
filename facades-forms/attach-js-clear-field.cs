using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

public class Program
{
    public static void Main()
    {
        // 1. Create a sample PDF with a text box field named DiscountCode
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 650);
            TextBoxField discountField = new TextBoxField(page, rect);
            discountField.PartialName = "DiscountCode";
            document.Form.Add(discountField);
            document.Save("input.pdf");
        }

        // 2. Open the created PDF and attach JavaScript that clears the field on focus
        using (Document pdfDocument = new Document("input.pdf"))
        {
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(pdfDocument);

            // JavaScript to clear the field when it receives focus
            string jsCode = "event.target.value='';";

            // Add the script to the field "DiscountCode"
            bool scriptAdded = formEditor.AddFieldScript("DiscountCode", jsCode);
            Console.WriteLine("JavaScript added: " + scriptAdded);

            // Save the modified PDF
            formEditor.Save("output.pdf");
        }
    }
}