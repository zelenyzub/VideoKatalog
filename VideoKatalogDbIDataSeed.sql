USE VideoKatalogDb;
GO

INSERT INTO Categories (Name) VALUES
(N'Akcija'),
(N'Drama'),
(N'Komedija'),
(N'Dokumentarni'),
(N'Horor'),
(N'Sci-Fi');
GO

INSERT INTO Videos (Title, Description, Duration, URL, CategoryId, ImagePath, IsActive) VALUES

-- AKCIJA (1)
(N'John Wick', N'Bivši plaćeni ubica vraća se u svet kriminala.', N'1h 41min', N'https://www.youtube.com/watch?v=C0BMx-qxsP4', 1, N'VideoCoverImages/JohnWick.jpeg', 1),
(N'Mad Max: Fury Road', N'Postapokaliptična potera kroz pustinju.', N'2h 00min', N'https://www.youtube.com/watch?v=hEJnMQG9ev8', 1, N'VideoCoverImages/MadMax.jpeg', 1),
(N'Gladiator', N'Rimski general postaje gladijator.', N'2h 35min', N'https://www.youtube.com/watch?v=P5ieIbInFpg', 1, N'VideoCoverImages/Gladiator.jpeg', 1),
(N'The Dark Knight', N'Sukob Batmana i Jokera u Gotamu.', N'2h 32min', N'https://www.youtube.com/watch?v=EXeTwQWrcwY', 1, N'VideoCoverImages/TheDarkKnight.jpeg', 1),

-- DRAMA (2)
(N'Forrest Gump', N'Životna priča jednostavnog čoveka.', N'2h 22min', N'https://www.youtube.com/watch?v=bLvqoHBptjg', 2, N'VideoCoverImages/ForrestGump.jpeg', 1),
(N'The Shawshank Redemption', N'Prijateljstvo i nada u zatvoru.', N'2h 22min', N'https://www.youtube.com/watch?v=PLl99DlL6b4', 2, N'VideoCoverImages/TheShawshankRedemption.jpeg', 1),
(N'Breaking Bad', N'Profesor hemije postaje narko-bos.', N'5 sezona', N'https://www.youtube.com/watch?v=HhesaQXLuRY', 2, N'VideoCoverImages/BreakingBad.jpeg', 1),
(N'The Godfather', N'Mafijaška porodica Korleone.', N'2h 55min', N'https://www.youtube.com/watch?v=UaVTIH8mujA', 2, N'VideoCoverImages/TheGodfather.jpeg', 1),

-- KOMEDIJA (3)
(N'The Hangover', N'Nezaboravna momačka noć u Las Vegasu.', N'1h 40min', N'https://www.youtube.com/watch?v=tlize92ffnY', 3, N'VideoCoverImages/TheHangover.jpeg', 1),
(N'Friends', N'Život i prijateljstvo u Njujorku.', N'10 sezona', N'https://www.youtube.com/watch?v=Zg2LCD5QOJs', 3, N'VideoCoverImages/Friends.jpeg', 1),
(N'Home Alone', N'Dečak sam protiv provalnika.', N'1h 43min', N'https://www.youtube.com/watch?v=jEDaVHmw7r4', 3, N'VideoCoverImages/HomeAlone.jpeg', 1),
(N'Deadpool', N'Antiheroj sa crnim humorom.', N'1h 48min', N'https://www.youtube.com/watch?v=Xithigfg7dA', 3, N'VideoCoverImages/Deadpool.jpeg', 1),

-- DOKUMENTARNI (4)
(N'Planet Earth', N'Dokumentarac o prirodi i planeti Zemlji.', N'11 epizoda', N'https://www.youtube.com/watch?v=c8aFcHFu8QM', 4, N'VideoCoverImages/PlanetEarth.jpeg', 1),
(N'The Last Dance', N'Karijera Michaela Jordana.', N'10 epizoda', N'https://www.youtube.com/watch?v=N9Z9JtNcCWY', 4, N'VideoCoverImages/TheLastDance.jpeg', 1),
(N'Cosmos', N'Putovanje kroz svemir i nauku.', N'13 epizoda', N'https://www.youtube.com/watch?v=QoNSU9o6464', 4, N'VideoCoverImages/Cosmos.jpeg', 1),
(N'Our Planet', N'Prirodna čuda sveta.', N'8 epizoda', N'https://www.youtube.com/watch?v=aETNYyrqNYE', 4, N'VideoCoverImages/OurPlanet.jpeg', 1),

-- HOROR (5)
(N'The Conjuring', N'Paranormalni istražitelji u akciji.', N'1h 52min', N'https://www.youtube.com/watch?v=ejMMn0t58Lc', 5, N'VideoCoverImages/TheConjuring.jpeg', 1),
(N'It', N'Zlo koje vreba decu.', N'2h 15min', N'https://www.youtube.com/watch?v=xKJmEC5ieOk', 5, N'VideoCoverImages/It.jpeg', 1),
(N'Hereditary', N'Mračna porodična tragedija.', N'2h 07min', N'https://www.youtube.com/watch?v=V6wWKNij_1M', 5, N'VideoCoverImages/Hereditary.jpeg', 1),
(N'The Exorcist', N'Opsednutost i egzorcizam.', N'2h 02min', N'https://www.youtube.com/watch?v=9wj62u817FA', 5, N'VideoCoverImages/TheExorcist.jpeg', 1),

-- SCI-FI (6)
(N'Interstellar', N'Putovanje kroz svemir i vreme.', N'2h 49min', N'https://www.youtube.com/watch?v=zSWdZVtXT7E', 6, N'VideoCoverImages/Interstellar.jpeg', 1),
(N'Inception', N'Krađa ideja kroz snove.', N'2h 28min', N'https://www.youtube.com/watch?v=YoHD9XEInc0', 6, N'VideoCoverImages/Inception.jpeg', 1),
(N'The Matrix', N'Stvarna priroda realnosti.', N'2h 16min', N'https://www.youtube.com/watch?v=vKQi3bBA1y8', 6, N'VideoCoverImages/TheMatrix.jpeg', 1),
(N'Stranger Things', N'Natprirodni događaji u malom gradu.', N'4 sezone', N'https://www.youtube.com/watch?v=mnd7sFt5c3A', 6, N'VideoCoverImages/StrangerThings.jpeg', 1),
(N'Blade Runner 2049', N'Budućnost čovečanstva i androida.', N'2h 44min', N'https://www.youtube.com/watch?v=gCcx85zbxz4', 6, N'VideoCoverImages/BladeRunner.jpeg', 1);
GO
