using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static CSV_to_XML.Program;

namespace CSV_to_XML {
    public class SettlementConverter {
		public static void settlement_CSVtoXML(string csvInput, string csvOutput, string xmlOutput, string sceneFileInput, string sceneFileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			List<SettlementRecord> records = new CsvReader(new StreamReader(csvInput), CultureInfo.InvariantCulture).GetRecords<SettlementRecord>().ToList();

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";

			XmlReaderSettings settings1 = new XmlReaderSettings();

			using (XmlReader sceneReader = XmlReader.Create(sceneFileInput, settings1)) {
				using (XmlWriter sceneWriter = XmlWriter.Create(sceneFileOutput, settings)) {
					while (sceneReader.Read()) {
						if (sceneReader.NodeType == XmlNodeType.Element) {
							if (sceneReader.Name.Equals("scene")) {
								sceneWriter.WriteStartElement("scene");
								sceneWriter.WriteAttributes(sceneReader, false);
								//Do stuff here

								while (sceneReader.Read()) {
									if (sceneReader.NodeType.Equals(XmlNodeType.Element)) {
										//Console.WriteLine("Currently at node {0}", sceneReader.Name);
										if (sceneReader.Name.Equals("entities")) {
											sceneWriter.WriteStartElement("entities");
											sceneWriter.WriteAttributes(sceneReader, false);
											List<SettlementRecord> addedRecords = new List<SettlementRecord>();



											while (sceneReader.Read()) {
												if (sceneReader.NodeType.Equals(XmlNodeType.EndElement) && sceneReader.Name.Equals("entities")) {
													//Handle non-initialized entities

													int positionCounterX = 1;
													int positionCounterY = 1;

													foreach (SettlementRecord record in records.Except(addedRecords)) {
														//break;//Get rid of this for the more so... experimental portions of the code.

														sceneWriter.WriteStartElement("game_entity");

														sceneWriter.WriteAttributeString("name", record.id);

														switch (record.GetSettlementType()) {
															case "castle":
																sceneWriter.WriteAttributeString("old_prefab_name", "map_icon_castle_battania");
																break;
															case "village":
																sceneWriter.WriteAttributeString("old_prefab_name", "map_icon_full_battania_village");
																break;
															case "town":
																sceneWriter.WriteAttributeString("old_prefab_name", "__empty_object");
																break;
														}
														sceneWriter.WriteAttributeString("season_mask", "255");

														//Nodes - Start

														//Transform - Start
														sceneWriter.WriteStartElement("transform");

														string[] positions = new string[3];
														positions[2] = "50.000"; //Constant Parameter

														if (record.posX.Equals("") || record.posY.Equals("")) {
															if (record.GetSettlementType().Equals("village")) {//record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2)
																											   //if (record.id.Contains("castle_village_")) {
																											   //	writer.WriteAttributeString(null, "trade_bound", null, record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
																											   //	writer.WriteAttributeString(null, "bound", null, record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
																											   //} else {
																											   //	writer.WriteAttributeString(null, "trade_bound", null, record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
																											   //	writer.WriteAttributeString(null, "bound", null, record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
																											   //}

																foreach (SettlementRecord r in records) {
																	if (r.id.Equals(record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2)) || r.id.Equals(record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2))) {
																		if (r.auto_assigned || r.posX.Equals("") || r.posY.Equals("")) break;

																		double centerX = Double.Parse(r.posX);
																		double centerY = Double.Parse(r.posY);

																		int offsetX = (r.auto_position_counter % 3) - 1;
																		int offsetY = (int)Math.Floor(r.auto_position_counter / 3d) - 1;

																		positions[0] = (4 * offsetX + centerX).ToString();
																		positions[1] = (4 * offsetY + centerY).ToString();

																		record.posX = positions[0];
																		record.posY = positions[1];

																		r.auto_position_counter++;
																		if (r.auto_position_counter == 4) r.auto_position_counter++;

																		goto AfterPositionHandling;
																	}
																}
															}

															record.auto_assigned = true;

															positions[0] = (4 * positionCounterX).ToString();
															positions[1] = (4 * positionCounterY).ToString();

															record.posX = positions[0];
															record.posY = positions[1];

															positionCounterX++;
															if (positionCounterX > 200) {
																positionCounterX = 1;
																positionCounterY++;
															}
														} else {
															positions[0] = record.posX;
															positions[1] = record.posY;
														}

													AfterPositionHandling:
														sceneWriter.WriteAttributeString("position", positions[0] + ", " + positions[1] + ", " + positions[2]);
														sceneWriter.WriteAttributeString("rotation_euler", "0.000, 0.000, -1.601");
														//Skip Scale

														sceneWriter.WriteEndElement();
														//Transform - End

														//Physics - Start
														sceneWriter.WriteStartElement("physics");
														sceneWriter.WriteAttributeString("mass", "1.000");
														sceneWriter.WriteEndElement();
														//Physics - End

														//Children - Start
														sceneWriter.WriteStartElement("children");
														switch (record.GetSettlementType()) {
															case "castle":
																using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "sampleCastleChildren.xml", settings1)) {
																	while (sample.Read() && (!sample.NodeType.Equals(XmlNodeType.Element) || (sample.NodeType.Equals(XmlNodeType.Element) && !sample.Name.Equals("child_entities")))) ;
																	while (sample.Read()) {
																		if (sample.NodeType.Equals(XmlNodeType.Element)) {
																			if (sample.Name.Equals("child_entities")) continue;
																			sceneWriter.WriteNode(sample, false);
																		}
																	}
																}
																break;
															case "village":
																using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "sampleVillageChildren.xml", settings1)) {
																	while (sample.Read() && (!sample.NodeType.Equals(XmlNodeType.Element) || (sample.NodeType.Equals(XmlNodeType.Element) && !sample.Name.Equals("child_entities")))) ;
																	while (sample.Read()) {
																		if (sample.NodeType.Equals(XmlNodeType.Element)) {
																			if (sample.Name.Equals("child_entities")) continue;
																			sceneWriter.WriteNode(sample, false);
																		}
																	}
																}
																break;
															case "town":
																using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "sampleTownChildren.xml", settings1)) {
																	while (sample.Read() && (!sample.NodeType.Equals(XmlNodeType.Element) || (sample.NodeType.Equals(XmlNodeType.Element) && !sample.Name.Equals("child_entities")))) ;
																	while (sample.Read()) {
																		if (sample.NodeType.Equals(XmlNodeType.Element)) {
																			if (sample.Name.Equals("child_entities")) continue;
																			sceneWriter.WriteNode(sample, false);
																		}
																	}
																}
																break;
															default:
																Console.WriteLine("You... Shouldn't be here. {0}", record.GetSettlementType());
																break;
														}
														sceneWriter.WriteEndElement();
														//Children - End

														sceneWriter.WriteEndElement();


														//OTHER METHOD STARTS HERE
														/*
														using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory+"sampleCastle.xml", settings1)) {
															while (sample.Read()) {
																if (!sample.NodeType.Equals(XmlNodeType.Element)) continue;
																if (!sample.Name.Equals("game_entity")) continue;
																//if (sample.GetAttribute("name") == null) continue;

																//Console.WriteLine("Hello? {0}", record.id);

																sceneWriter.WriteStartElement("game_entity");

																sceneWriter.WriteAttributeString("name", record.id);

																if (sample.GetAttribute("old_prefab_name") != null) sceneWriter.WriteAttributeString("old_prefab_name", sample.GetAttribute("old_prefab_name"));
																if (sample.GetAttribute("prefab") != null) sceneWriter.WriteAttributeString("prefab", sample.GetAttribute("prefab"));
																if (sample.GetAttribute("season_mask") != null) sceneWriter.WriteAttributeString("season_mask", sample.GetAttribute("season_mask"));

																while (sample.Read()) {
																	if (sample.NodeType.Equals(XmlNodeType.EndElement) && sample.Name.Equals("game_entity")) {
																		sceneWriter.WriteEndElement();
																		break;
																	}
																	//if (sample.Name.Equals("children")) Console.WriteLine("Marco 4"); ;
																	//Console.WriteLine("We're reading a {1} node of type {0}.", sample.Name, sample.NodeType);
																	if (sample.NodeType.Equals(XmlNodeType.Element)) {
																		//Console.WriteLine("Marco 3");
																		switch (sample.Name) {
																			case "transform":
																				sceneWriter.WriteStartElement("transform");

																				string[] positions = sample.GetAttribute("position").Split(", ");

																				if(record.posX.Equals("") || record.posY.Equals("")) {
																					positions[0] = positionCounterX.ToString();
																					positions[1] = positionCounterY.ToString();

																					record.posX = positions[0];
																					record.posY = positions[1];

																					positionCounterX++;
																					if(positionCounterX > 200) {
																						positionCounterX = 1;
																						positionCounterY++;
                                                                                    }
																				} else {
																					positions[0] = record.posX;
																					positions[1] = record.posY;
																				}


																				sceneWriter.WriteAttributeString("position", positions[0] + ", " + positions[1] + ", " + positions[2]);

																				sceneWriter.WriteAttributeString("rotation_euler", "0.000, 0.000, -1.601");

																				//if (sample.GetAttribute("rotation_euler") != null) sceneWriter.WriteAttributeString("rotation_euler", sample.GetAttribute("rotation_euler"));
																				//if (sample.GetAttribute("scale") != null) sceneWriter.WriteAttributeString("scale", sample.GetAttribute("scale"));

																				sceneWriter.WriteEndElement();
																				break;
																			case "physics":
																				sceneWriter.WriteNode(sample, false);
																				break;
																			case "children":
																				sceneWriter.WriteStartElement("children");
																				//sceneWriter.WriteAttributes(sample, false);
																				while(sample.Read()) {
																					if(sample.NodeType.Equals(XmlNodeType.EndElement)&&sample.Name.Equals("children")) {
																						//Console.WriteLine("Marco 1");
																						sceneWriter.WriteEndElement();
																						break;
																					}
																					//Console.WriteLine("Marco 2");
																					if (sample.NodeType.Equals(XmlNodeType.Element) && sample.Name.Equals("game_entity")) sceneWriter.WriteNode(sample, false);
																				}
																				break;
																			default:
																				Console.WriteLine("You shouldn't be seeing this. We're reading a type {0}.", sample.Name);
																				//sceneWriter.WriteNode(sample, false);
																				return;
																		}
																	}
																}
															}
														}
														*/
													}
													//Stop
													sceneWriter.WriteEndElement();
													break;
												}

												if (sceneReader.NodeType.Equals(XmlNodeType.Element)) {
													//Do stuff here 2: Electric Boogaloo
													if (!sceneReader.Name.Equals("game_entity")) continue;
													if (sceneReader.GetAttribute("name") == null) continue;


													if (sceneReader.GetAttribute("name").StartsWith("castle_") || sceneReader.GetAttribute("name").StartsWith("village_") || sceneReader.GetAttribute("name").StartsWith("town_")) {
														/* // Check to ensure all attributes are taken care of.
														int count=1;
														if (sceneReader.GetAttribute("old_prefab_name") != null) count++;
														if (sceneReader.GetAttribute("prefab") != null) count++;
														if (sceneReader.GetAttribute("season_mask") != null) count++;
														if (sceneReader.AttributeCount != count) {
															for (int i = 0; i < sceneReader.AttributeCount; i++) Console.Write((i + 1) + ": " + sceneReader.GetAttribute(i) + "\n");
														}
														//*/
														SettlementRecord record = null;
														foreach (SettlementRecord r in records) {
															if (r.id_scene.Equals(sceneReader.GetAttribute("name"))) {
																record = r;
																break;
															}
														}
														foreach (SettlementRecord r in records) {
															if (r.id.Equals(sceneReader.GetAttribute("name"))) {
																if (record != null && record != r) {
																	throw new Exception("There already exists an id " + r.id + ".");
																}
																record = r;
																break;
															}
														}

														if (record != null) {
															addedRecords.Add(record);


															sceneWriter.WriteStartElement("game_entity");

															sceneWriter.WriteAttributeString("name", record.id);

															if (sceneReader.GetAttribute("old_prefab_name") != null) sceneWriter.WriteAttributeString("old_prefab_name", sceneReader.GetAttribute("old_prefab_name"));
															if (sceneReader.GetAttribute("prefab") != null) sceneWriter.WriteAttributeString("prefab", sceneReader.GetAttribute("prefab"));
															if (sceneReader.GetAttribute("season_mask") != null) sceneWriter.WriteAttributeString("season_mask", sceneReader.GetAttribute("season_mask"));

															while (sceneReader.Read()) {
																if (sceneReader.NodeType.Equals(XmlNodeType.EndElement) && sceneReader.Name.Equals("game_entity")) {
																	sceneWriter.WriteEndElement();
																	break;
																}
																if (sceneReader.NodeType.Equals(XmlNodeType.Element)) {
																	switch (sceneReader.Name) {
																		case "transform":
																			/*
																			int count = 0;
																			if (sceneReader.GetAttribute("position") != null) count++;
																			if (sceneReader.GetAttribute("rotation_euler") != null) count++;
																			if (count!=2) {
																				for (int i = 0; i < sceneReader.AttributeCount; i++) Console.Write((i + 1) + " - "+record.id+": " + sceneReader.GetAttribute(i) + "\n");
																			}
																			*/
																			sceneWriter.WriteStartElement("transform");

																			string[] positions = sceneReader.GetAttribute("position").Split(", ");

																			record.posX = positions[0];
																			record.posY = positions[1];

																			sceneWriter.WriteAttributeString("position", sceneReader.GetAttribute("position"));
																			if (sceneReader.GetAttribute("rotation_euler") != null) sceneWriter.WriteAttributeString("rotation_euler", sceneReader.GetAttribute("rotation_euler"));
																			if (sceneReader.GetAttribute("scale") != null) sceneWriter.WriteAttributeString("scale", sceneReader.GetAttribute("scale"));

																			sceneWriter.WriteEndElement();
																			break;
																		default:
																			sceneWriter.WriteNode(sceneReader, false);
																			break;
																	}
																}
															}
														} else { //TODO Handle excess castles and whatnot.
															sceneReader.Skip();
														}
													} else {
														sceneWriter.WriteNode(sceneReader, false);
													}



													//Stop doing stuff 2: Electric Boogaloo
												}
											}
										} else {
											sceneWriter.WriteNode(sceneReader, false);
										}
									}
								}

								//Stop doing stuff
								sceneWriter.WriteEndElement();
							}
						}
					}

					sceneWriter.Flush();
				}
			}

			using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
				csvWriter.WriteRecords(records);
				csvWriter.Flush();
			}

			using (XmlWriter writer = XmlWriter.Create(xmlOutput, settings)) {
				writer.WriteStartElement("Settlements");
				foreach (SettlementRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Settlement");

					//Changes
					record.Comp_Town_is_castle = record.Comp_Town_is_castle.ToLower();

					//Defaults
					if (record.culture.Equals("")) record.culture = "youkai";

					//Temporary


					//Write
					writer.WriteAttributeString(null, "id", null, record.id);
					writer.WriteAttributeString(null, "name", null, "{=Settlements.Settlement." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Settlements.Settlement." + record.id + ".name", record.name);

					if (!record.owner.Equals("")) writer.WriteAttributeString(null, "owner", null, "Faction." + record.owner);
					writer.WriteAttributeString(null, "posX", null, record.posX);
					writer.WriteAttributeString(null, "posY", null, record.posY);
					if (!record.prosperity.Equals("")) writer.WriteAttributeString(null, "prosperity", null, record.prosperity);
					if (!record.culture.Equals("")) writer.WriteAttributeString(null, "culture", null, "Culture." + record.culture);
					if (!record.gate_posX.Equals("")) writer.WriteAttributeString(null, "gate_posX", null, record.gate_posX);
					if (!record.gate_posX.Equals("")) writer.WriteAttributeString(null, "gate_posY", null, record.gate_posY);
					if (!record.gate_rotation.Equals("")) writer.WriteAttributeString(null, "gate_rotation", null, record.gate_rotation);
					if (!record.type.Equals("")) writer.WriteAttributeString(null, "type", null, record.type);
					if (!record.text.Equals("")) writer.WriteAttributeString(null, "text", null, "{=Settlements.Settlement." + record.id + ".text}" + record.text);

					if (!record.text.Equals("")) writeLocalizationNode(localizationWriter, "Settlements.Settlement." + record.id + ".text", record.text);

					if (!record.Comp_Town_is_castle.Equals("") || !record.Comp_Village_village_type.Equals("") || !record.Comp_Hideout_map_icon.Equals("")) {
						writer.WriteStartElement("Components");

						if (!record.Comp_Town_is_castle.Equals("")) { // Town or Castle
							writer.WriteStartElement("Town");

							//Set up defaults
							if (record.Comp_Town_level.Equals("")) record.Comp_Town_level = "1";
							if (record.Comp_Town_background_crop_position.Equals("")) record.Comp_Town_background_crop_position = "0.0";
							if (record.Comp_Town_background_mesh.Equals("")) record.Comp_Town_background_mesh = "menu_empire_1";
							if (record.Comp_Town_wait_mesh.Equals("")) record.Comp_Town_wait_mesh = "wait_empire_town";
							if (record.Comp_Town_gate_rotation.Equals("")) record.Comp_Town_gate_rotation = "0.908";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id.Replace("_", "_comp_"));
							writer.WriteAttributeString(null, "is_castle", null, record.Comp_Town_is_castle);
							writer.WriteAttributeString(null, "level", null, record.Comp_Town_level);
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Town_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Town_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Town_wait_mesh);
							writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Town_gate_rotation);

							writer.WriteEndElement();
						}

						if (!record.Comp_Village_village_type.Equals("")) { // Village
							writer.WriteStartElement("Village");

							//Set up defaults
							if (record.Comp_Village_hearth.Equals("")) record.Comp_Village_hearth = "200"; // Seeded Random?
							if (record.Comp_Village_background_crop_position.Equals("")) record.Comp_Village_background_crop_position = "0.0";
							if (record.Comp_Village_background_mesh.Equals("")) record.Comp_Village_background_mesh = "gui_bg_village_empire";
							if (record.Comp_Village_castle_background_mesh.Equals("")) record.Comp_Village_castle_background_mesh = "gui_bg_castle_empire";
							if (record.Comp_Village_wait_mesh.Equals("")) record.Comp_Village_wait_mesh = "wait_empire_village";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id.Replace("village_", "village_comp_"));
							writer.WriteAttributeString(null, "village_type", null, "VillageType." + record.Comp_Village_village_type);
							if (!record.Comp_Village_gate_rotation.Equals("")) writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Village_gate_rotation);
							writer.WriteAttributeString(null, "hearth", null, record.Comp_Village_hearth);
							if (record.id.Contains("castle_village_")) {
								writer.WriteAttributeString(null, "trade_bound", null, "Settlement." + record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
								writer.WriteAttributeString(null, "bound", null, "Settlement." + record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
							} else {
								writer.WriteAttributeString(null, "trade_bound", null, "Settlement." + record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
								writer.WriteAttributeString(null, "bound", null, "Settlement." + record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
							}
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Village_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Village_background_mesh);
							writer.WriteAttributeString(null, "castle_background_mesh", null, record.Comp_Village_castle_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Village_wait_mesh);

							writer.WriteEndElement();
						}

						if (!record.Comp_Hideout_map_icon.Equals("")) { // Hideout
							writer.WriteStartElement("Hideout");

							//Set up defaults
							if (record.Comp_Hideout_scene_name.Equals("")) record.Comp_Hideout_scene_name = "bandit_forest";
							if (record.Comp_Hideout_background_crop_position.Equals("")) record.Comp_Hideout_background_crop_position = "0.0";
							if (record.Comp_Hideout_background_mesh.Equals("")) record.Comp_Hideout_background_mesh = "empire_twn_scene_bg";
							if (record.Comp_Hideout_wait_mesh.Equals("")) record.Comp_Hideout_wait_mesh = "wait_hideout_forest";
							if (record.Comp_Hideout_gate_rotation.Equals("")) record.Comp_Hideout_gate_rotation = "0.0";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id);
							writer.WriteAttributeString(null, "map_icon", null, record.Comp_Hideout_map_icon);
							writer.WriteAttributeString(null, "scene_name", null, record.Comp_Hideout_scene_name);
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Town_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Hideout_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Town_wait_mesh);
							writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Hideout_gate_rotation);

							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.Locations_complex_template.Equals("")) {
						writer.WriteStartElement("Locations");
						writer.WriteAttributeString(null, "complex_template", null, "LocationComplexTemplate." + record.Locations_complex_template);
						if (!record.Locations_Location0_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location0_id);
							if (!record.Locations_Location0_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location0_scene_name);
							if (!record.Locations_Location0_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location0_scene_name_1);
							if (!record.Locations_Location0_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location0_scene_name_2);
							if (!record.Locations_Location0_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location0_scene_name_3);
							if (!record.Locations_Location0_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location0_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location1_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location1_id);
							if (!record.Locations_Location1_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location1_scene_name);
							if (!record.Locations_Location1_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location1_scene_name_1);
							if (!record.Locations_Location1_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location1_scene_name_2);
							if (!record.Locations_Location1_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location1_scene_name_3);
							if (!record.Locations_Location1_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location1_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location2_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location2_id);
							if (!record.Locations_Location2_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location2_scene_name);
							if (!record.Locations_Location2_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location2_scene_name_1);
							if (!record.Locations_Location2_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location2_scene_name_2);
							if (!record.Locations_Location2_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location2_scene_name_3);
							if (!record.Locations_Location2_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location2_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location3_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location3_id);
							if (!record.Locations_Location3_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location3_scene_name);
							if (!record.Locations_Location3_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location3_scene_name_1);
							if (!record.Locations_Location3_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location3_scene_name_2);
							if (!record.Locations_Location3_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location3_scene_name_3);
							if (!record.Locations_Location3_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location3_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location4_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location4_id);
							if (!record.Locations_Location4_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location4_scene_name);
							if (!record.Locations_Location4_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location4_scene_name_1);
							if (!record.Locations_Location4_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location4_scene_name_2);
							if (!record.Locations_Location4_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location4_scene_name_3);
							if (!record.Locations_Location4_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location4_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location5_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location5_id);
							if (!record.Locations_Location5_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location5_scene_name);
							if (!record.Locations_Location5_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location5_scene_name_1);
							if (!record.Locations_Location5_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location5_scene_name_2);
							if (!record.Locations_Location5_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location5_scene_name_3);
							if (!record.Locations_Location5_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location5_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location6_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location6_id);
							if (!record.Locations_Location6_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location6_scene_name);
							if (!record.Locations_Location6_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location6_scene_name_1);
							if (!record.Locations_Location6_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location6_scene_name_2);
							if (!record.Locations_Location6_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location6_scene_name_3);
							if (!record.Locations_Location6_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location6_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location7_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location7_id);
							if (!record.Locations_Location7_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location7_scene_name);
							if (!record.Locations_Location7_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location7_scene_name_1);
							if (!record.Locations_Location7_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location7_scene_name_2);
							if (!record.Locations_Location7_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location7_scene_name_3);
							if (!record.Locations_Location7_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location7_max_prosperity);

							writer.WriteEndElement();
						}
						writer.WriteEndElement();

					}

					bool automatic_common_areas_village = true;

					if (!record.CommonAreas_Area0_type.Equals("") || !record.CommonAreas_Area1_type.Equals("") || !record.CommonAreas_Area2_type.Equals("") || (!record.Comp_Village_village_type.Equals("") && automatic_common_areas_village)) {
						writer.WriteStartElement("CommonAreas");

						if (!record.Comp_Village_village_type.Equals("") && automatic_common_areas_village) {
							if (record.CommonAreas_Area0_type.Equals("")) {
								record.CommonAreas_Area0_type = "Pasture";
								record.CommonAreas_Area0_name = "Pasture";
							}
							if (record.CommonAreas_Area1_type.Equals("")) {
								record.CommonAreas_Area1_type = "Thicket";
								record.CommonAreas_Area1_name = "Thicket";
							}
							if (record.CommonAreas_Area2_type.Equals("")) {
								record.CommonAreas_Area2_type = "Bog";
								record.CommonAreas_Area2_name = "Bog";
							}
						}

						if (!record.CommonAreas_Area0_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area0_type);
							writer.WriteAttributeString(null, "name", null, "{=CommonAreas.Area." + record.CommonAreas_Area0_name + "}" + record.CommonAreas_Area0_name);
							writer.WriteEndElement();
						}
						if (!record.CommonAreas_Area1_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area1_type);
							writer.WriteAttributeString(null, "name", null, "{=CommonAreas.Area." + record.CommonAreas_Area0_name + "}" + record.CommonAreas_Area1_name);
							writer.WriteEndElement();
						}
						if (!record.CommonAreas_Area2_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area2_type);
							writer.WriteAttributeString(null, "name", null, "{=CommonAreas.Area." + record.CommonAreas_Area0_name + "}" + record.CommonAreas_Area2_name);
							writer.WriteEndElement();
						}
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.Flush();
			}
		}

		public static void settlement_XMLtoCSV(string xmlInput, string csvOutput) {
			List<SettlementRecord> records = new List<SettlementRecord>();

			using (XmlReader xmlReader = XmlReader.Create(xmlInput)) {
				while (xmlReader.Read()) {
					if (xmlReader.NodeType != XmlNodeType.Element) continue;

					if (xmlReader.Name.Equals("Settlement")) {
						SettlementRecord record = new SettlementRecord();

						record.id = xmlReader.GetAttribute("id");
						record.name = xmlReader.GetAttribute("name").Split("}").Last();
						if (xmlReader.GetAttribute("owner") != null) record.owner = xmlReader.GetAttribute("owner").Split(".").Last();

						record.posX = xmlReader.GetAttribute("posX");
						record.posY = xmlReader.GetAttribute("posY");

						if (xmlReader.GetAttribute("prosperity") != null) record.prosperity = xmlReader.GetAttribute("prosperity");
						if (xmlReader.GetAttribute("culture") != null) record.culture = xmlReader.GetAttribute("culture").Split(".").Last();

						if (xmlReader.GetAttribute("gate_posX") != null) record.gate_posX = xmlReader.GetAttribute("gate_posX");
						if (xmlReader.GetAttribute("gate_posY") != null) record.gate_posY = xmlReader.GetAttribute("gate_posY");

						if (xmlReader.GetAttribute("gate_rotation") != null) record.gate_rotation = xmlReader.GetAttribute("gate_rotation");

						if (xmlReader.GetAttribute("type") != null) record.type = xmlReader.GetAttribute("type");
						if (xmlReader.GetAttribute("text") != null) record.text = xmlReader.GetAttribute("text").Split("}").Last();

						SettlementNodeSwitch:
						while (xmlReader.Read()) {
							if (!xmlReader.NodeType.Equals(XmlNodeType.Element)) continue;
							switch (xmlReader.Name) {
								case "Components":
									while (xmlReader.Read()) {
										if (!xmlReader.NodeType.Equals(XmlNodeType.Element)) continue;
										switch (xmlReader.Name) {
											case "Town":
												if (xmlReader.GetAttribute("is_castle") != null) record.Comp_Town_is_castle = xmlReader.GetAttribute("is_castle");
												if (xmlReader.GetAttribute("level") != null) record.Comp_Town_level = xmlReader.GetAttribute("level");
												if (xmlReader.GetAttribute("background_crop_position") != null) record.Comp_Town_background_crop_position = xmlReader.GetAttribute("background_crop_position");
												if (xmlReader.GetAttribute("background_mesh") != null) record.Comp_Town_background_mesh = xmlReader.GetAttribute("background_mesh");
												if (xmlReader.GetAttribute("wait_mesh") != null) record.Comp_Town_wait_mesh = xmlReader.GetAttribute("wait_mesh");
												if (xmlReader.GetAttribute("gate_rotation") != null) record.Comp_Town_gate_rotation = xmlReader.GetAttribute("gate_rotation");

												break;
											case "Village":
												if (xmlReader.GetAttribute("background_crop_position") != null) record.Comp_Village_background_crop_position = xmlReader.GetAttribute("background_crop_position");
												if (xmlReader.GetAttribute("background_mesh") != null) record.Comp_Village_background_mesh = xmlReader.GetAttribute("background_mesh");
												if (xmlReader.GetAttribute("bound") != null) record.Comp_Village_bound = xmlReader.GetAttribute("bound").Split(".").Last();
												if (xmlReader.GetAttribute("castle_background_mesh") != null) record.Comp_Village_castle_background_mesh = xmlReader.GetAttribute("castle_background_mesh");
												if (xmlReader.GetAttribute("gate_rotation") != null) record.Comp_Village_gate_rotation = xmlReader.GetAttribute("gate_rotation");
												if (xmlReader.GetAttribute("hearth") != null) record.Comp_Village_hearth = xmlReader.GetAttribute("hearth");
												if (xmlReader.GetAttribute("trade_bound") != null) record.Comp_Village_trade_bound = xmlReader.GetAttribute("trade_bound").Split(".").Last();
												if (xmlReader.GetAttribute("village_type") != null) record.Comp_Village_village_type = xmlReader.GetAttribute("village_type").Split(".").Last();
												if (xmlReader.GetAttribute("wait_mesh") != null) record.Comp_Village_wait_mesh = xmlReader.GetAttribute("wait_mesh");

												break;
											case "Hideout":
												if (xmlReader.GetAttribute("background_crop_position") != null) record.Comp_Hideout_background_crop_position = xmlReader.GetAttribute("background_crop_position");
												if (xmlReader.GetAttribute("background_mesh") != null) record.Comp_Hideout_background_mesh = xmlReader.GetAttribute("background_mesh");
												if (xmlReader.GetAttribute("gate_rotation") != null) record.Comp_Hideout_gate_rotation = xmlReader.GetAttribute("gate_rotation");
												if (xmlReader.GetAttribute("map_icon") != null) record.Comp_Hideout_map_icon = xmlReader.GetAttribute("map_icon");
												if (xmlReader.GetAttribute("scene_name") != null) record.Comp_Hideout_scene_name = xmlReader.GetAttribute("scene_name");
												if (xmlReader.GetAttribute("wait_mesh") != null) record.Comp_Hideout_wait_mesh = xmlReader.GetAttribute("wait_mesh");

												break;
											default:
												goto SettlementNodeSwitch;
										}
									}
									break;
								case "Locations":
									if (xmlReader.GetAttribute("complex_template") != null) record.Locations_complex_template = xmlReader.GetAttribute("complex_template").Split(".").Last();

									int counterLocations = 0;
									while (xmlReader.Read()) {
										if (!xmlReader.NodeType.Equals(XmlNodeType.Element)) continue;
										switch (xmlReader.Name) {
											case "Location":
												switch (counterLocations) {
													case 0:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location0_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location0_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location0_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location0_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location0_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location0_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 1:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location1_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location1_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location1_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location1_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location1_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location1_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 2:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location2_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location2_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location2_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location2_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location2_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location2_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 3:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location3_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location3_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location3_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location3_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location3_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location3_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 4:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location4_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location4_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location4_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location4_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location4_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location4_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 5:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location5_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location5_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location5_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location5_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location5_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location5_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 6:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location6_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location6_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location6_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location6_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location6_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location6_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
													case 7:
														if (xmlReader.GetAttribute("id") != null) record.Locations_Location7_id = xmlReader.GetAttribute("id");
														if (xmlReader.GetAttribute("max_prosperity") != null) record.Locations_Location7_max_prosperity = xmlReader.GetAttribute("max_prosperity");
														if (xmlReader.GetAttribute("scene_name") != null) record.Locations_Location7_scene_name = xmlReader.GetAttribute("scene_name");
														if (xmlReader.GetAttribute("scene_name_1") != null) record.Locations_Location7_scene_name_1 = xmlReader.GetAttribute("scene_name_1");
														if (xmlReader.GetAttribute("scene_name_2") != null) record.Locations_Location7_scene_name_2 = xmlReader.GetAttribute("scene_name_2");
														if (xmlReader.GetAttribute("scene_name_3") != null) record.Locations_Location7_scene_name_3 = xmlReader.GetAttribute("scene_name_3");
														break;
												}
												counterLocations++;
												break;
											default:
												goto SettlementNodeSwitch;
										}
									}
									break;
								case "CommonAreas":
									int counterCommonAreas = 0;
									while (xmlReader.Read()) {
										if (!xmlReader.NodeType.Equals(XmlNodeType.Element)) continue;
										switch (xmlReader.Name) {
											case "Area":
												switch (counterCommonAreas) {
													case 0:
														if (xmlReader.GetAttribute("name") != null) record.CommonAreas_Area0_name = xmlReader.GetAttribute("name").Split("}").Last();
														if (xmlReader.GetAttribute("type") != null) record.CommonAreas_Area0_type = xmlReader.GetAttribute("type");
														break;
													case 1:
														if (xmlReader.GetAttribute("name") != null) record.CommonAreas_Area1_name = xmlReader.GetAttribute("name").Split("}").Last();
														if (xmlReader.GetAttribute("type") != null) record.CommonAreas_Area1_type = xmlReader.GetAttribute("type");
														break;
													case 2:
														if (xmlReader.GetAttribute("name") != null) record.CommonAreas_Area2_name = xmlReader.GetAttribute("name").Split("}").Last();
														if (xmlReader.GetAttribute("type") != null) record.CommonAreas_Area2_type = xmlReader.GetAttribute("type");
														break;
												}
												counterCommonAreas++;
												break;
											default:
												goto SettlementNodeSwitch;
										}
									}
									break;
								default:
									goto AfterSettlementNodeSwitch;
							}
						}
					AfterSettlementNodeSwitch:
						records.Add(record);
					}
				}
			}

			using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
				csvWriter.WriteRecords(records);
				csvWriter.Flush();
			}
		}

		public class SettlementRecord {
			//Basic
			public string id { get; set; }
			public string id_scene { get; set; }
			public string name { get; set; }
			public string owner { get; set; }
			public string posX { get; set; }
			public string posY { get; set; }
			public string prosperity { get; set; }
			public string culture { get; set; }
			public string gate_posX { get; set; }
			public string gate_posY { get; set; }
			public string gate_rotation { get; set; }
			public string type { get; set; }
			public string text { get; set; }

			//Components
			//Town
			//public string Comp_Town_id { get; set; } //Handle this dynamically
			public string Comp_Town_is_castle { get; set; }
			public string Comp_Town_level { get; set; }
			public string Comp_Town_background_crop_position { get; set; }
			public string Comp_Town_background_mesh { get; set; }
			public string Comp_Town_wait_mesh { get; set; }
			public string Comp_Town_gate_rotation { get; set; }
			//Village
			//public string Comp_village_id { get; set; }
			public string Comp_Village_village_type { get; set; }
			public string Comp_Village_gate_rotation { get; set; }
			public string Comp_Village_hearth { get; set; }
			public string Comp_Village_trade_bound { get; set; }
			public string Comp_Village_bound { get; set; }
			public string Comp_Village_background_crop_position { get; set; }
			public string Comp_Village_background_mesh { get; set; }
			public string Comp_Village_castle_background_mesh { get; set; }
			public string Comp_Village_wait_mesh { get; set; }
			//Hideout
			//public string Comp_Hideout_id { get; set; }
			public string Comp_Hideout_map_icon { get; set; }
			public string Comp_Hideout_scene_name { get; set; }
			public string Comp_Hideout_background_crop_position { get; set; }
			public string Comp_Hideout_background_mesh { get; set; }
			public string Comp_Hideout_wait_mesh { get; set; }
			public string Comp_Hideout_gate_rotation { get; set; }

			//Locations
			public string Locations_complex_template { get; set; }
			//Location 0
			public string Locations_Location0_id { get; set; }
			public string Locations_Location0_scene_name { get; set; }
			public string Locations_Location0_scene_name_1 { get; set; }
			public string Locations_Location0_scene_name_2 { get; set; }
			public string Locations_Location0_scene_name_3 { get; set; }
			public string Locations_Location0_max_prosperity { get; set; }
			//Location 1
			public string Locations_Location1_id { get; set; }
			public string Locations_Location1_scene_name { get; set; }
			public string Locations_Location1_scene_name_1 { get; set; }
			public string Locations_Location1_scene_name_2 { get; set; }
			public string Locations_Location1_scene_name_3 { get; set; }
			public string Locations_Location1_max_prosperity { get; set; }
			//Location 2
			public string Locations_Location2_id { get; set; }
			public string Locations_Location2_scene_name { get; set; }
			public string Locations_Location2_scene_name_1 { get; set; }
			public string Locations_Location2_scene_name_2 { get; set; }
			public string Locations_Location2_scene_name_3 { get; set; }
			public string Locations_Location2_max_prosperity { get; set; }
			//Location 3
			public string Locations_Location3_id { get; set; }
			public string Locations_Location3_scene_name { get; set; }
			public string Locations_Location3_scene_name_1 { get; set; }
			public string Locations_Location3_scene_name_2 { get; set; }
			public string Locations_Location3_scene_name_3 { get; set; }
			public string Locations_Location3_max_prosperity { get; set; }
			//Location 4
			public string Locations_Location4_id { get; set; }
			public string Locations_Location4_scene_name { get; set; }
			public string Locations_Location4_scene_name_1 { get; set; }
			public string Locations_Location4_scene_name_2 { get; set; }
			public string Locations_Location4_scene_name_3 { get; set; }
			public string Locations_Location4_max_prosperity { get; set; }
			//Location 5
			public string Locations_Location5_id { get; set; }
			public string Locations_Location5_scene_name { get; set; }
			public string Locations_Location5_scene_name_1 { get; set; }
			public string Locations_Location5_scene_name_2 { get; set; }
			public string Locations_Location5_scene_name_3 { get; set; }
			public string Locations_Location5_max_prosperity { get; set; }
			//Location 6
			public string Locations_Location6_id { get; set; }
			public string Locations_Location6_scene_name { get; set; }
			public string Locations_Location6_scene_name_1 { get; set; }
			public string Locations_Location6_scene_name_2 { get; set; }
			public string Locations_Location6_scene_name_3 { get; set; }
			public string Locations_Location6_max_prosperity { get; set; }
			//Location 7
			public string Locations_Location7_id { get; set; }
			public string Locations_Location7_scene_name { get; set; }
			public string Locations_Location7_scene_name_1 { get; set; }
			public string Locations_Location7_scene_name_2 { get; set; }
			public string Locations_Location7_scene_name_3 { get; set; }
			public string Locations_Location7_max_prosperity { get; set; }
			//CommonAreas
			//Area 0
			public string CommonAreas_Area0_type { get; set; }
			public string CommonAreas_Area0_name { get; set; }
			//Area 1
			public string CommonAreas_Area1_type { get; set; }
			public string CommonAreas_Area1_name { get; set; }
			//Area 2
			public string CommonAreas_Area2_type { get; set; }
			public string CommonAreas_Area2_name { get; set; }

			public override bool Equals(object obj) {
				if (obj is SettlementRecord) return this.id.Equals(((SettlementRecord)obj).id);
				return base.Equals(obj);
			}

			public override int GetHashCode() {
				return HashCode.Combine(id, name);
			}

			public string GetSettlementType() {//Just in case... return other if not village in case of crash.
				if (Comp_Town_is_castle.ToLowerInvariant().Equals("true")) return "castle";
				if (Comp_Town_is_castle.ToLowerInvariant().Equals("false")) return "town";
				if (!Comp_Village_village_type.Equals("")) return "village";

				return "other";
			}
			public bool auto_assigned = false;
			public int auto_position_counter = 0;
		}
	}
}
