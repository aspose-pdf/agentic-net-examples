using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with two pages
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();

            // Add page labels to the pages
            PageLabel label1 = new PageLabel();
            label1.StartingValue = 1;
            label1.Prefix = "A-";
            doc.PageLabels.UpdateLabel(1, label1);

            PageLabel label2 = new PageLabel();
            label2.StartingValue = 2;
            label2.Prefix = "A-";
            doc.PageLabels.UpdateLabel(2, label2);

            // Store original labels for later verification
            List<string> originalLabels = new List<string>();
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                PageLabel lbl = doc.PageLabels.GetLabel(i);
                if (lbl != null)
                {
                    originalLabels.Add(lbl.Prefix + lbl.StartingValue);
                }
                else
                {
                    originalLabels.Add(i.ToString());
                }
            }

            // Create a simple PNG image to be used as a stamp (1x1 pixel red dot)
            string imagePath = "stamp.png";
            byte[] pngBytes = Convert.FromBase64String(
                "iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==");
            File.WriteAllBytes(imagePath, pngBytes);

            // Create the image stamp and configure its appearance
            ImageStamp imgStamp = new ImageStamp(imagePath);
            imgStamp.XIndent = 100;
            imgStamp.YIndent = 100;
            imgStamp.Width = 50;
            imgStamp.Height = 50;
            imgStamp.Opacity = 0.5f;

            // Apply the stamp to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(imgStamp);
            }

            // Verify that page labels are unchanged after stamping
            bool labelsUnchanged = true;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                PageLabel lbl = doc.PageLabels.GetLabel(i);
                string current = (lbl != null) ? lbl.Prefix + lbl.StartingValue : i.ToString();
                if (current != originalLabels[i - 1])
                {
                    labelsUnchanged = false;
                    break;
                }
            }
            Console.WriteLine("Page labels unchanged after stamping: " + labelsUnchanged);

            // Save the stamped PDF
            doc.Save("output.pdf");
        }
    }
}
