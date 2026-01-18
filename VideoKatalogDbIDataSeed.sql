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
(N'John Wick', N'Bivši plaćeni ubica vraća se u svet kriminala.', N'1h 41min', N'https://example.com/john-wick', 1, N'assets/media/auth/bg11.png', 1),
(N'Mad Max: Fury Road', N'Postapokaliptična potera kroz pustinju.', N'2h 00min', N'https://example.com/mad-max', 1, N'assets/media/auth/bg7.jpg', 1),
(N'Gladiator', N'Rimski general postaje gladijator.', N'2h 35min', N'https://example.com/gladiator', 1, N'assets/media/auth/bg3.jpg', 1),
(N'The Dark Knight', N'Sukob Batmana i Jokera u Gotamu.', N'2h 32min', N'https://example.com/dark-knight', 1, N'assets/media/auth/bg1.jpg', 1),

-- DRAMA (2)
(N'Forrest Gump', N'Životna priča jednostavnog čoveka.', N'2h 22min', N'https://example.com/forrest-gump', 2, N'assets/media/auth/bg5.jpg', 1),
(N'The Shawshank Redemption', N'Prijateljstvo i nada u zatvoru.', N'2h 22min', N'https://example.com/shawshank', 2, N'assets/media/auth/bg7.jpg', 1),
(N'Breaking Bad', N'Profesor hemije postaje narko-bos.', N'5 sezona', N'https://example.com/breaking-bad', 2, N'assets/media/auth/bg11.png', 1),
(N'The Godfather', N'Mafijaška porodica Korleone.', N'2h 55min', N'https://example.com/godfather', 2, N'assets/media/auth/bg3.jpg', 1),

-- KOMEDIJA (3)
(N'The Hangover', N'Nezaboravna momačka noć u Las Vegasu.', N'1h 40min', N'https://example.com/hangover', 3, N'assets/media/auth/bg5.jpg', 1),
(N'Friends', N'Život i prijateljstvo u Njujorku.', N'10 sezona', N'https://example.com/friends', 3, N'assets/media/auth/bg3.jpg', 1),
(N'Home Alone', N'Dečak sam protiv provalnika.', N'1h 43min', N'https://example.com/home-alone', 3, N'assets/media/auth/bg11.png', 1),
(N'Deadpool', N'Antiheroj sa crnim humorom.', N'1h 48min', N'https://example.com/deadpool', 3, N'assets/media/auth/bg1.jpg', 1),

-- DOKUMENTARNI (4)
(N'Planet Earth', N'Dokumentarac o prirodi i planeti Zemlji.', N'11 epizoda', N'https://example.com/planet-earth', 4, N'assets/media/auth/bg5.jpg', 1),
(N'The Last Dance', N'Karijera Michaela Jordana.', N'10 epizoda', N'https://example.com/last-dance', 4, N'assets/media/auth/bg7.jpg', 1),
(N'Cosmos', N'Putovanje kroz svemir i nauku.', N'13 epizoda', N'https://example.com/cosmos', 4, N'assets/media/auth/bg3.jpg', 1),
(N'Our Planet', N'Prirodna čuda sveta.', N'8 epizoda', N'https://example.com/our-planet', 4, N'assets/media/auth/bg11.png', 1),

-- HOROR (5)
(N'The Conjuring', N'Paranormalni istražitelji u akciji.', N'1h 52min', N'https://example.com/conjuring', 5, N'assets/media/auth/bg3.jpg', 1),
(N'It', N'Zlo koje vreba decu.', N'2h 15min', N'https://example.com/it', 5, N'assets/media/auth/bg5.jpg', 1),
(N'Hereditary', N'Mračna porodična tragedija.', N'2h 07min', N'https://example.com/hereditary', 5, N'assets/media/auth/bg11.png', 1),
(N'The Exorcist', N'Opsednutost i egzorcizam.', N'2h 02min', N'https://example.com/exorcist', 5, N'assets/media/auth/bg7.jpg', 1),

-- SCI-FI (6)
(N'Interstellar', N'Putovanje kroz svemir i vreme.', N'2h 49min', N'https://example.com/interstellar', 6, N'assets/media/auth/bg7.jpg', 1),
(N'Inception', N'Krađa ideja kroz snove.', N'2h 28min', N'https://example.com/inception', 6, N'assets/media/auth/bg3.jpg', 1),
(N'The Matrix', N'Stvarna priroda realnosti.', N'2h 16min', N'https://example.com/matrix', 6, N'assets/media/auth/bg5.jpg', 1),
(N'Stranger Things', N'Natprirodni događaji u malom gradu.', N'4 sezone', N'https://example.com/stranger-things', 6, N'assets/media/auth/bg3.jpg', 1),
(N'Blade Runner 2049', N'Budućnost čovečanstva i androida.', N'2h 44min', N'https://example.com/blade-runner', 6, N'assets/media/auth/bg5.jpg', 1);
GO


USE VideoKatalogDb;
GO

-- Ocene za 10 videa
INSERT INTO Rates (Value, Timestamp, VideoId, UserId) VALUES
-- Video 1: John Wick
(5, '2025-11-10 14:23:00', 1, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(3, '2025-12-20 09:30:00', 1, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 2: Mad Max: Fury Road
(5, '2025-10-15 12:00:00', 2, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(4, '2025-11-01 15:45:00', 2, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 3: Gladiator
(5, '2025-12-02 20:30:00', 3, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(2, '2025-12-10 10:00:00', 3, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 4: The Dark Knight
(5, '2025-11-18 14:15:00', 4, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(4, '2025-11-25 17:40:00', 4, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 5: Forrest Gump
(5, '2025-08-30 09:00:00', 5, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(4, '2025-09-10 10:20:00', 5, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 6: The Shawshank Redemption
(5, '2025-10-10 12:00:00', 6, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(4, '2025-11-01 13:45:00', 6, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 7: Breaking Bad
(5, '2025-09-20 15:00:00', 7, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(3, '2025-09-25 16:00:00', 7, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 8: The Godfather
(5, '2025-10-12 18:30:00', 8, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(4, '2025-10-15 19:00:00', 8, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 9: The Hangover
(4, '2025-12-05 21:00:00', 9, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(5, '2025-12-10 22:15:00', 9, N'7b61879c-70f2-475d-9d92-b89ba9976578'),

-- Video 10: Friends
(5, '2025-11-01 10:10:00', 10, N'5d9f9e2f-103a-4025-8f1d-65719fd0bfb6'),
(5, '2025-11-10 12:30:00', 10, N'7b61879c-70f2-475d-9d92-b89ba9976578');
GO
