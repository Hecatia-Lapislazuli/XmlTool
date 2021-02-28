//
// HeroConverter.cs
//
// Author:
//       Urist_McAurelian <Discord: Urist_McAurelian#2289>
//
// Copyright (c) 2021 Urist_McAurelian
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static XmlTool.Program;

namespace XmlTool {
	//Functionally same result after running through twice.
    public class HeroConverter {
		public static void CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			HeroRecord record = new HeroRecord();
			IEnumerable<HeroRecord> records = csv.EnumerateRecords(record);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Heroes");

				writeHeadderComment(writer);

				foreach (HeroRecord heroRecord in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Hero");

					//Changes
					record.is_noble = record.is_noble.ToLower();
					record.alive = record.alive.ToLower();

					//Temporary

					//Write
					writer.WriteAttributeString(null, "id", null, heroRecord.id);
					if (!heroRecord.is_noble.Equals("")) writer.WriteAttributeString(null, "is_noble", null, heroRecord.is_noble);
					if (!heroRecord.father.Equals("")) writer.WriteAttributeString(null, "father", null, heroRecord.father);
					if (!heroRecord.mother.Equals("")) writer.WriteAttributeString(null, "mother", null, heroRecord.mother);
					writer.WriteAttributeString(null, "faction", null, "Faction." + heroRecord.faction);
					if (!heroRecord.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, heroRecord.banner_key);
					if (!heroRecord.spouse.Equals("")) writer.WriteAttributeString(null, "spouse", null, heroRecord.spouse);
					if (!heroRecord.alive.Equals("")) writer.WriteAttributeString(null, "alive", null, heroRecord.alive);

					if (!heroRecord.text.Equals("")) writer.WriteAttributeString("text", GetLocalizedString(localizationWriter, record.text, record.id, "text", "Heroes.Hero"));

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public static void XMLtoCSV(string xmlInput, string csvOutput) {
			List<HeroRecord> records = new List<HeroRecord>();

			using (XmlReader root = XmlReader.Create(xmlInput)) {
				root.MoveToContent();
				while (root.Read()) {
					if (root.NodeType.Equals(XmlNodeType.Element)) break;
                }

				while (!root.EOF) {
					if (root.NodeType != XmlNodeType.Element) {
						root.Read();
						continue;
					}
					HeroRecord record = new HeroRecord();

					record.id = root.GetAttribute("id");
					record.is_noble = root.GetAttribute("is_noble");
					record.father = TrimD(root.GetAttribute("father"));
					record.mother = TrimD(root.GetAttribute("mother"));
					record.faction = TrimD(root.GetAttribute("faction"));
					record.banner_key = root.GetAttribute("banner_key");
					record.spouse = TrimD(root.GetAttribute("spouse"));
					record.alive = root.GetAttribute("alive");
					record.text = TrimB(root.GetAttribute("text"));

					records.Add(record);
					root.Read();
				}
				using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
					csvWriter.WriteRecords(records);
					csvWriter.Flush();
				}
			}
		}
		
		public class HeroRecord {
			public string id { get; set; }
			public string is_noble { get; set; }
			public string father { get; set; }
			public string mother { get; set; }
			public string faction { get; set; }
			public string banner_key { get; set; }
			public string spouse { get; set; }
			public string alive { get; set; }
			public string text { get; set; }
		}
	}
}
