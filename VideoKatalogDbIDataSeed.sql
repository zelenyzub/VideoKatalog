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

INSERT INTO Videos (Title, Description, Duration, URL, CategoryId, ImagePath) VALUES

-- AKCIJA (1)
(N'John Wick', N'Bivši plaćeni ubica vraća se u svet kriminala.', N'1h 41min', N'https://example.com/john-wick', 1, N'assets/media/auth/bg11.png'),
(N'Mad Max: Fury Road', N'Postapokaliptična potera kroz pustinju.', N'2h 00min', N'https://example.com/mad-max', 1, N'assets/media/auth/bg7.jpg'),
(N'Gladiator', N'Rimski general postaje gladijator.', N'2h 35min', N'https://example.com/gladiator', 1, N'assets/media/auth/bg3.jpg'),
(N'The Dark Knight', N'Sukob Batmana i Jokera u Gotamu.', N'2h 32min', N'https://example.com/dark-knight', 1, N'assets/media/auth/bg1.jpg'),

-- DRAMA (2)
(N'Forrest Gump', N'Životna priča jednostavnog čoveka.', N'2h 22min', N'https://example.com/forrest-gump', 2, N'assets/media/auth/bg5.jpg'),
(N'The Shawshank Redemption', N'Prijateljstvo i nada u zatvoru.', N'2h 22min', N'https://example.com/shawshank', 2, N'assets/media/auth/bg7.jpg'),
(N'Breaking Bad', N'Profesor hemije postaje narko-bos.', N'5 sezona', N'https://example.com/breaking-bad', 2, N'assets/media/auth/bg11.png'),
(N'The Godfather', N'Mafijaška porodica Korleone.', N'2h 55min', N'https://example.com/godfather', 2, N'assets/media/auth/bg3.jpg'),

-- KOMEDIJA (3)
(N'The Hangover', N'Nezaboravna momačka noć u Las Vegasu.', N'1h 40min', N'https://example.com/hangover', 3, N'assets/media/auth/bg5.jpg'),
(N'Friends', N'Život i prijateljstvo u Njujorku.', N'10 sezona', N'https://example.com/friends', 3, N'assets/media/auth/bg3.jpg'),
(N'Home Alone', N'Dečak sam protiv provalnika.', N'1h 43min', N'https://example.com/home-alone', 3, N'assets/media/auth/bg11.png'),
(N'Deadpool', N'Antiheroj sa crnim humorom.', N'1h 48min', N'https://example.com/deadpool', 3, N'assets/media/auth/bg1.jpg'),

-- DOKUMENTARNI (4)
(N'Planet Earth', N'Dokumentarac o prirodi i planeti Zemlji.', N'11 epizoda', N'https://example.com/planet-earth', 4, N'assets/media/auth/bg5.jpg'),
(N'The Last Dance', N'Karijera Michaela Jordana.', N'10 epizoda', N'https://example.com/last-dance', 4, N'assets/media/auth/bg7.jpg'),
(N'Cosmos', N'Putovanje kroz svemir i nauku.', N'13 epizoda', N'https://example.com/cosmos', 4, N'assets/media/auth/bg3.jpg'),
(N'Our Planet', N'Prirodna čuda sveta.', N'8 epizoda', N'https://example.com/our-planet', 4, N'assets/media/auth/bg11.png'),

-- HOROR (5)
(N'The Conjuring', N'Paranormalni istražitelji u akciji.', N'1h 52min', N'https://example.com/conjuring', 5, N'assets/media/auth/bg3.jpg'),
(N'It', N'Zlo koje vreba decu.', N'2h 15min', N'https://example.com/it', 5, N'assets/media/auth/bg5.jpg'),
(N'Hereditary', N'Mračna porodična tragedija.', N'2h 07min', N'https://example.com/hereditary', 5, N'assets/media/auth/bg11.png'),
(N'The Exorcist', N'Opsednutost i egzorcizam.', N'2h 02min', N'https://example.com/exorcist', 5, N'assets/media/auth/bg7.jpg'),

-- SCI-FI (6)
(N'Interstellar', N'Putovanje kroz svemir i vreme.', N'2h 49min', N'https://example.com/interstellar', 6, N'assets/media/auth/bg7.jpg'),
(N'Inception', N'Krađa ideja kroz snove.', N'2h 28min', N'https://example.com/inception', 6, N'assets/media/auth/bg3.jpg'),
(N'The Matrix', N'Stvarna priroda realnosti.', N'2h 16min', N'https://example.com/matrix', 6, N'assets/media/auth/bg5.jpg'),
(N'Stranger Things', N'Natprirodni događaji u malom gradu.', N'4 sezone', N'https://example.com/stranger-things', 6, N'assets/media/auth/bg3.jpg'),
(N'Blade Runner 2049', N'Budućnost čovečanstva i androida.', N'2h 44min', N'https://example.com/blade-runner', 6, N'assets/media/auth/bg5.jpg');
GO
