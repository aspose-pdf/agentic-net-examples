using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Create a sample XML file that maps page numbers to background images
            string xmlPath = "backgrounds.xml";
            CreateSampleXml(xmlPath);

            // Load the XML mapping
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList pageNodes = xmlDoc.SelectNodes("/Backgrounds/Page");

            foreach (XmlNode pageNode in pageNodes)
            {
                int pageNumber = int.Parse(pageNode.Attributes["Number"].Value);
                string imagePath = pageNode.Attributes["ImagePath"].Value;

                if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                {
                    Page page = doc.Pages[pageNumber];
                    BackgroundArtifact bgArtifact = new BackgroundArtifact();
                    bgArtifact.IsBackground = true;
                    bgArtifact.SetImage(imagePath);
                    page.Artifacts.Add(bgArtifact);
                }
            }

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }

    private static void CreateSampleXml(string path)
    {
        // Create minimal 1x1 PNG image bytes (transparent pixel)
        byte[] pngBytes = new byte[] {
            0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
            0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
            0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
            0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
            0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
            0x54,0x78,0x9C,0x63,0x60,0x00,0x00,0x00,
            0x02,0x00,0x01,0xE2,0x21,0xBC,0x33,0x00,
            0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
            0x42,0x60,0x82 };

        // Write three identical placeholder images
        string img1 = "bg1.png";
        string img2 = "bg2.png";
        string img3 = "bg3.png";
        File.WriteAllBytes(img1, pngBytes);
        File.WriteAllBytes(img2, pngBytes);
        File.WriteAllBytes(img3, pngBytes);

        // Build XML structure
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("Backgrounds");
        xmlDoc.AppendChild(root);

        for (int i = 1; i <= 3; i++)
        {
            XmlElement pageElem = xmlDoc.CreateElement("Page");
            pageElem.SetAttribute("Number", i.ToString());
            pageElem.SetAttribute("ImagePath", "bg" + i + ".png");
            root.AppendChild(pageElem);
        }

        xmlDoc.Save(path);
    }
}