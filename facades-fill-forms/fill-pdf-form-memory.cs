using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

namespace FillPdfFormMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with a single text box field.
            using (Document templateDoc = new Document())
            {
                Page page = templateDoc.Pages.Add();
                // Define rectangle for the field (lower-left x, lower-left y, upper-right x, upper-right y).
                Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
                TextBoxField nameField = new TextBoxField(page, fieldRect);
                nameField.PartialName = "Name";
                templateDoc.Form.Add(nameField);

                // Save the template PDF into a memory stream.
                using (MemoryStream templateStream = new MemoryStream())
                {
                    templateDoc.Save(templateStream);
                    templateStream.Position = 0;

                    // Prepare data to fill the form.
                    DataTable data = new DataTable("FormData");
                    DataColumn nameColumn = new DataColumn("Name", typeof(string));
                    data.Columns.Add(nameColumn);
                    DataRow row1 = data.NewRow();
                    row1["Name"] = "John Doe";
                    data.Rows.Add(row1);
                    DataRow row2 = data.NewRow();
                    row2["Name"] = "Jane Smith";
                    data.Rows.Add(row2);

                    // Fill the PDF using AutoFiller.
                    using (AutoFiller filler = new AutoFiller())
                    {
                        filler.BindPdf(templateStream);
                        filler.ImportDataTable(data);
                        using (MemoryStream resultStream = new MemoryStream())
                        {
                            filler.Save(resultStream);
                            resultStream.Position = 0;
                            // Write the filled PDF to a file.
                            using (FileStream file = new FileStream("filled.pdf", FileMode.Create, FileAccess.Write))
                            {
                                resultStream.CopyTo(file);
                            }
                        }
                    }
                }
            }
        }
    }
}
