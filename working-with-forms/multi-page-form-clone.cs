using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

namespace MultiPageFormClone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a single form field and save it.
            using (Document sourceDoc = new Document())
            {
                // Add the first page.
                sourceDoc.Pages.Add();

                // Define the rectangle for the text box field.
                Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

                // Create a TextBoxField on the first page.
                TextBoxField baseField = new TextBoxField(sourceDoc.Pages[1], fieldRect);
                baseField.PartialName = "BaseField";

                // Add the field to the form on page 1.
                sourceDoc.Form.Add(baseField, 1);

                // Save the template PDF.
                sourceDoc.Save("input.pdf");
            }

            // Step 2: Open the template and clone the field onto additional pages.
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Retrieve the original field by its name and cast to Field.
                Aspose.Pdf.Forms.Field originalField = pdfDoc.Form["BaseField"] as Aspose.Pdf.Forms.Field;
                if (originalField == null)
                {
                    Console.WriteLine("Original field not found.");
                    return;
                }

                // Define total number of pages (including the first one).
                int totalPages = 3; // Evaluation mode allows up to 4 pages/fields.

                for (int pageNumber = 2; pageNumber <= totalPages; pageNumber++)
                {
                    // Add a new blank page.
                    pdfDoc.Pages.Add();

                    // Clone the original field onto the new page with a unique name.
                    string newFieldName = "Field_Page" + pageNumber;
                    pdfDoc.Form.Add(originalField, newFieldName, pageNumber);
                }

                // Save the final document.
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
