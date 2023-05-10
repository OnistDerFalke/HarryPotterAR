using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Board
{
    class BoardManager
    {
        [SerializeField] 
        private List<Board> boards = new List<Board>() {
            new Board(0, 50.0f, 50.0f),
            new Board(1, 25.0f, 25.0f),
            new Board(2, 25.0f, 25.0f)
        };

        public List<Board> Boards { get => boards; }

        public void InitBoard()
        {
            InitFirstBoard();
            InitSecondBoard();
            InitThirdBoard();

            AddNeighborsToFieldOnFirstBoard();
            AddNeighborsToFieldOnSecondBoard();
            AddNeighborsToFieldOnThirdBoard();

            AddPortalFieldsToFieldOnFirstBoard();
            AddPortalFieldsToFieldOnSecondBoard();
            AddPortalFieldsToFieldOnThirdBoard();
        }

        private void InitFirstBoard()
        {
            int boardId = 0;

            // Start
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(3.7f, 14.4f), new Vector2(9.0f, 14.4f), new Vector2(9.0f, 11.0f), new Vector2(3.7f, 11.0f)), 0));
            // Ręce
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(3.4f, 17.5f), new Vector2(5.9f, 17.5f), new Vector2(5.9f, 14.9f), new Vector2(3.4f, 14.9f)), 1));
            // Flaga
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(6.2f, 17.5f), new Vector2(8.9f, 17.5f), new Vector2(8.9f, 14.9f), new Vector2(6.2f, 14.9f)), 2));
            // Wieża zegarowa
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.2f, 17.3f), new Vector2(11.8f, 17.3f), new Vector2(11.8f, 14.8f), new Vector2(9.2f, 14.8f)), 3, true));
            // Skrzydło szpitalne
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(13.3f, 17.0f), new Vector2(15.8f, 17.0f), new Vector2(15.8f, 15.2f), new Vector2(13.3f, 15.2f)), 4));
            // Posąg jednookiej wiedźmy
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(16.5f, 19.8f), new Vector2(20.0f, 19.8f), new Vector2(20.0f, 16.0f), new Vector2(16.5f, 16.0f)), 5));
            // Księga nad wieżą zegarową
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.4f, 19.5f), new Vector2(11.7f, 19.5f), new Vector2(11.7f, 17.8f), new Vector2(9.4f, 17.8f)), 6));
            // Pokój wspólny Slytherinu
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(10.4f, 22.0f), 1.5f), 7, true));
            // Gwiazdka obok pokoju wspólnego Slytherinu
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(12.2f, 23.0f), new Vector2(14.5f, 23.0f), new Vector2(14.5f, 20.3f), new Vector2(12.2f, 20.3f)), 8));
            // Zaklęcie obok gwiazki obok pokoju wspólnego Slytherinu
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(14.9f, 23.0f), new Vector2(17.2f, 23.0f), new Vector2(17.2f, 20.3f), new Vector2(14.9f, 20.3f)), 9));
            // Księga obok pokoju wspólnego Ravenclaw
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(17.6f, 22.9f), new Vector2(19.6f, 22.9f), new Vector2(19.6f, 20.4f), new Vector2(17.6f, 20.4f)), 10));
            // Pokój wspólny Ravenclaw
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(21.75f, 21.7f), 1.7f), 11, true));
            // Wydarzenie pod pokojem wspólnym Ravenclaw
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(20.3f, 19.8f), new Vector2(22.9f, 19.8f), new Vector2(22.9f, 17.7f), new Vector2(20.3f, 17.7f)), 12));
            // Gwiazdka pod eliksirem pod pokojem wspólnym Ravenclaw
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(20.4f, 17.5f), new Vector2(23.0f, 17.5f), new Vector2(23.0f, 15.0f), new Vector2(20.4f, 15.0f)), 13));
            // Zaklęcie nad pokojem wspólnym Hufflepuff
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(20.4f, 14.5f), new Vector2(22.9f, 14.5f), new Vector2(22.9f, 12.5f), new Vector2(20.4f, 12.5f)), 14));
            // Pokój wspólny Hufflepuff
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(22.6f, 9.7f), 2.0f), 15, true));
            // Gabinet Dumbledore'a
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(20.6f, 6.8f), 1.5f), 16));
            // Sprawowanie obok pokoju wspólnego Hufflepuff
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(17.6f, 11.9f), new Vector2(20.0f, 11.9f), new Vector2(19.7f, 8.9f), new Vector2(17.7f, 8.9f)), 17));
            // Pojedynek obok sprawowania obok pokoju wspólnego Hufflepuff
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(14.8f, 11.8f), new Vector2(17.4f, 11.8f), new Vector2(17.3f, 8.9f), new Vector2(14.8f, 8.9f)), 18));
            // Gwiazdka obok pokoju wspólnego Griffindoru
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(12.1f, 11.8f), new Vector2(14.6f, 11.8f), new Vector2(14.6f, 9.0f), new Vector2(12.1f, 9.0f)), 19));
            // Pokój wspólny Griffindoru
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(10.5f, 10.3f), 1.6f), 20, true));
            // Eliksir nad pokojem wspólnym Griffindoru
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.3f, 14.3f), new Vector2(11.8f, 14.3f), new Vector2(11.8f, 12.0f), new Vector2(9.3f, 12.4f)), 21));
            // Most obok pokoju wspólnego Hufflepuff
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(25.6f, 8.9f), new Vector2(28.6f, 8.2f), new Vector2(29.1f, 6.5f), new Vector2(24.9f, 7.5f)), 22));

            // Eliksir po lewej stronie wielkiej sali
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(29.2f, 8.6f), new Vector2(31.4f, 9.2f), new Vector2(32.1f, 6.5f), new Vector2(30.0f, 5.85f)), 23));
            // Gwiazdka nad eliksirem po lewej stronie wielkiej sali
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(28.4f, 11.1f), new Vector2(30.5f, 11.9f), new Vector2(31.2f, 9.5f), new Vector2(29.1f, 8.8f)), 24));
            // Wydarzenie nad wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(31.0f, 12.0f), new Vector2(33.0f, 12.7f), new Vector2(33.9f, 10.3f), new Vector2(31.6f, 9.7f)), 25));
            // Gwiazdka między wydarzeniem i księgą nad wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(33.4f, 12.8f), new Vector2(35.4f, 13.5f), new Vector2(36.4f, 11.15f), new Vector2(34.3f, 10.5f)), 26));
            // Księga nad wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(36.1f, 13.6f), new Vector2(38.3f, 14.3f), new Vector2(38.9f, 11.9f), new Vector2(37.0f, 11.2f)), 27));
            // Pojedynek obok księgi nad wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(38.6f, 14.4f), new Vector2(41.3f, 15.15f), new Vector2(42.0f, 12.9f), new Vector2(39.3f, 12.0f)), 28));
            // Gwiazdka nad kuchnią obok wielkiej sali
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(41.5f, 15.3f), new Vector2(43.9f, 16.1f), new Vector2(44.4f, 13.8f), new Vector2(42.3f, 13.0f)), 29));
            // Kuchnia obok wielkiej sali
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(42.5f, 12.8f), new Vector2(44.5f, 13.6f), new Vector2(45.25f, 11.0f), new Vector2(43.1f, 10.3f)), 30));
            // Eliksir pod kuchnią obok wielkiej sali
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(43.2f, 10.0f), new Vector2(45.4f, 10.8f), new Vector2(46.1f, 8.45f), new Vector2(43.9f, 7.7f)), 31));
            // Sprawowanie obok gwiazdki pod wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(40.6f, 9.3f), new Vector2(42.8f, 9.9f), new Vector2(43.4f, 7.6f), new Vector2(41.4f, 6.9f)), 32));
            // Gwiazdka obok sprawowania pod wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(38.2f, 8.4f), new Vector2(40.3f, 9.1f), new Vector2(40.9f, 6.75f), new Vector2(39.0f, 6.2f)), 33));
            // Zaklęcie pod wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(35.5f, 7.6f), new Vector2(37.7f, 8.3f), new Vector2(38.4f, 6.0f), new Vector2(36.2f, 5.3f)), 34));
            // Sprawowanie obok zaklęcia pod wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(32.6f, 6.6f), new Vector2(35.1f, 7.5f), new Vector2(35.9f, 5.2f), new Vector2(33.2f, 4.5f)), 35));
            // Pojedynek obok sprawowania pod wielką salą
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(30.1f, 5.6f), new Vector2(32.2f, 6.3f), new Vector2(33.95f, 4.3f), new Vector2(30.55f, 3.6f)), 36));
            // Wielka sala
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(31.9f, 9.4f), new Vector2(42.0f, 12.3f), new Vector2(42.5f, 10.3f), new Vector2(32.5f, 7.2f)), 37));
            // Most nad wielką salą do części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(36.5f, 18.5f), new Vector2(38.2f, 18.5f), new Vector2(38.1f, 14.5f), new Vector2(36.8f, 14.4f)), 38));

            // Zaklęcie w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(36.3f, 21.3f), new Vector2(38.7f, 21.3f), new Vector2(38.7f, 19.0f), new Vector2(36.3f, 19.0f)), 39));
            // Gwiazdka obok gabinetu Snape'a w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(39.25f, 21.3f), new Vector2(41.2f, 21.3f), new Vector2(41.2f, 19.0f), new Vector2(39.25f, 19.0f)), 40));
            // Gabinet Snape'a w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(43.05f, 20.15f), 1.5f), 41, true));
            // Gwiazdka nad gabinetem Snape'a w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(41.8f, 24.0f), new Vector2(44.2f, 24.0f), new Vector2(44.2f, 21.8f), new Vector2(41.8f, 21.9f)), 42));
            // Czwarta wieża z pojedynkiem w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(43.05f, 25.7f), 1.5f), 43, true));
            // Wydarzenie w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(39.1f, 26.9f), new Vector2(41.0f, 26.9f), new Vector2(41.4f, 24.4f), new Vector2(39.1f, 24.4f)), 44));
            // Gwizdka obok wydarzenia w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(35.9f, 26.85f), new Vector2(38.5f, 26.85f), new Vector2(38.5f, 24.4f), new Vector2(25.9f, 24.4f)), 45));
            // Sprawowanie w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(33.0f, 26.85f), new Vector2(35.6f, 26.85f), new Vector2(35.6f, 24.4f), new Vector2(33.0f, 24.4f)), 46));
            // Gwizdka obok zachodniego skrzydła 3. piętro w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(30.6f, 26.95f), new Vector2(32.7f, 26.95f), new Vector2(32.7f, 24.4f), new Vector2(30.6f, 24.6f)), 47));
            // Zachodnie skrzydło 3. piętro w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(28.85f, 25.85f), 1.5f), 48, true));
            // Eliksir pod zachodnim skrzydłem 3. piętro w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(27.4f, 24.3f), new Vector2(30.0f, 24.3f), new Vector2(30.0f, 21.75f), new Vector2(27.4f, 21.75f)), 49));
            // Eliksiry - sala w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(28.85f, 20.15f), 1.5f), 50, true));
            // Księga obok eliksirów w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(30.5f, 21.25f), new Vector2(32.7f, 21.25f), new Vector2(32.7f, 18.9f), new Vector2(30.5f, 18.9f)), 51));
            // Gwiazdka obok księgi w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(33.1f, 21.3f), new Vector2(35.7f, 21.3f), new Vector2(35.7f, 18.9f), new Vector2(33.1f, 18.9f)), 52));
            // Pole w części z nauką Eliksirów
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(30.65f, 24.1f), new Vector2(41.1f, 24.1f), new Vector2(41.1f, 21.7f), new Vector2(30.65f, 21.7f)), 53));
            // Kamienny most
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(23.4f, 23.3f), new Vector2(27.2f, 23.5f), new Vector2(27.3f, 22.4f), new Vector2(23.9f, 22.2f)), 54));
            // Most między częścią z nauką Eliksirów a wieżą centralną
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(36.5f, 29.7f), new Vector2(38.0f, 29.7f), new Vector2(38.0f, 27.0f), new Vector2(36.5f, 27.0f)), 55));

            // Wieża centralna
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(37.4f, 31.7f), 1.6f), 56, true));
            // Zaklęcie obok wieży centralnej
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(39.3f, 32.4f), new Vector2(41.3f, 32.45f), new Vector2(41.4f, 29.9f), new Vector2(38.6f, 29.8f)), 57));
            // Transmutacja
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(43.1f, 31.1f), 1.5f), 58, true));
            // Pojedynek nad transmutacją
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(41.5f, 35.3f), new Vector2(44.2f, 35.3f), new Vector2(44.1f, 33.0f), new Vector2(41.55f, 32.8f)), 59));
            // Gwiazdka nad pojedynkiem nad transmutacją
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(41.55f, 37.9f), new Vector2(44.1f, 37.9f), new Vector2(44.1f, 35.7f), new Vector2(41.55f, 35.7f)), 60));
            // Wydarzenie obok dzwonnicy z zaklęciem
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(41.6f, 39.4f), new Vector2(44.2f, 40.7f), new Vector2(44.2f, 38.4f), new Vector2(41.6f, 38.4f)), 61));
            // Dzwonnica z zaklęciem
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(42.0f, 41.5f), 1.7f), 62, true));
            // Sprawowanie obok dzwonnicy z zaklęciem
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(41.7f, 46.4f), new Vector2(44.2f, 46.4f), new Vector2(44.2f, 44.1f), new Vector2(41.7f, 44.1f)), 63));
            // Zielarstwo
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(44.5f, 46.4f), new Vector2(46.7f, 46.4f), new Vector2(46.7f, 43.9f), new Vector2(44.5f, 43.9f)), 64));
            // +4PŻ obok zielarstwa
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(44.5f, 43.5f), new Vector2(46.8f, 43.5f), new Vector2(46.8f, 41.5f), new Vector2(44.5f, 41.5f)), 65));
            // Eliksir pod +4PŹ (ślepy zaułek)
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(44.5f, 41.2f), new Vector2(46.8f, 41.2f), new Vector2(46.8f, 38.6f), new Vector2(44.5f, 38.6f)), 66));
            // Gwiazdka między dzwonnicami
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(35.3f, 43.85f), new Vector2(39.3f, 43.85f), new Vector2(39.3f, 41.4f), new Vector2(35.3f, 41.4f)), 67));
            // Dzwonnica z eliksirem
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(32.8f, 41.6f), 1.7f), 68, true));
            // Wierzba bijąca
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(27.8f, 46.9f), new Vector2(33.3f, 46.9f), new Vector2(33.3f, 44.2f), new Vector2(27.8f, 44.2f)), 69));
            // Puste pole między dzwonnicą i zaklęciami i urokami
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(30.75f, 40.8f), new Vector2(33.0f, 39.5f), new Vector2(33.0f, 38.4f), new Vector2(30.7f, 39.0f)), 130));
            // Zaklęcia i uroki
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(31.5f, 37.1f), 1.5f), 70, true));
            // Zaklęcie pod zaklęciami i urokami
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(30.35f, 35.3f), new Vector2(33.0f, 35.3f), new Vector2(33.0f, 33.0f), new Vector2(30.35f, 33.0f)), 71));
            // Wieża ze sprawowaniem obok pokoju życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(31.7f, 31.3f), 1.5f), 72, true));
            // Eliksir obok wieży centralnej
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(33.45f, 32.5f), new Vector2(35.4f, 32.55f), new Vector2(36.0f, 29.8f), new Vector2(33.6f, 29.8f)), 73));

            // Księga pod pokojem życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(27.3f, 32.45f), new Vector2(30.0f, 32.4f), new Vector2(30.0f, 30.0f), new Vector2(27.5f, 30.0f)), 74));
            // Gwizdka obok księgi pod pokojem życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(24.7f, 32.45f), new Vector2(27.05f, 32.45f), new Vector2(27.05f, 29.95f), new Vector2(24.7f, 29.95f)), 75));
            // Pojedynek obok obrony przed czarną magią
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(21.9f, 32.5f), new Vector2(24.3f, 32.5f), new Vector2(24.3f, 29.9f), new Vector2(21.9f, 29.9f)), 76));
            // Obrona przed czarną magią
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(19.7f, 30.9f), 2.0f), 77, true));
            // Most wiszący
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(18.4f, 28.45f), new Vector2(20.6f, 28.3f), new Vector2(20.0f, 23.4f), new Vector2(18.25f, 23.4f)), 78));
            // Gwiazdka obok Komnaty Tajemnic
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(19.0f, 35.4f), new Vector2(21.5f, 35.6f), new Vector2(21.45f, 32.9f), new Vector2(19.0f, 32.85f)), 79));
            // Komnata Tajemnic
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(15.8f, 35.5f), new Vector2(18.65f, 35.4f), new Vector2(18.65f, 33.0f), new Vector2(15.9f, 33.0f)), 80));
            // Pokój życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(21.8f, 35.6f), new Vector2(30.1f, 35.6f), new Vector2(30.2f, 32.6f), new Vector2(21.8f, 32.6f)), 81));
            // Wieża z eliksirem obok pokoju życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(30.1f, 37.1f), 1.5f), 82, true));
            // Sprawowanie nad pokojem życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(21.9f, 38.2f), new Vector2(24.2f, 38.2f), new Vector2(24.1f, 35.75f), new Vector2(21.7f, 35.75f)), 83));
            // Gwizadka nad pokojem życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(24.5f, 38.15f), new Vector2(27.1f, 38.15f), new Vector2(27.1f, 35.85f), new Vector2(24.5f, 35.85f)), 84));
            // Zaklęcie nad pokojem życzeń
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(27.4f, 38.1f), new Vector2(30.0f, 38.1f), new Vector2(30.0f, 35.9f), new Vector2(27.6f, 35.9f)), 85));

            // Pojedynek pod biblioteką
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(19.0f, 40.7f), new Vector2(21.4f, 40.8f), new Vector2(21.3f, 38.7f), new Vector2(19.0f, 38.9f)), 86));
            // Biblioteka
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(20.1f, 42.6f), 1.8f), 87, true));
            // Dział ksiąg zakazanych
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(18.9f, 46.3f), new Vector2(21.45f, 46.3f), new Vector2(21.4f, 44.5f), new Vector2(43.9f, 44.4f)), 88));
            // Eliksir obok biblioteki
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(22.0f, 43.8f), new Vector2(24.2f, 43.8f), new Vector2(24.2f, 41.3f), new Vector2(22.0f, 41.3f)), 89));
            // Wydarzenie obok eliksiru obok biblioteki
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(24.6f, 43.75f), new Vector2(26.9f, 43.75f), new Vector2(26.9f, 41.3f), new Vector2(24.6f, 41.3f)), 90));
            // Gwiazdka obok dzwonnicy z eliksirem
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(27.3f, 43.8f), new Vector2(30.1f, 43.8f), new Vector2(30.1f, 41.3f), new Vector2(27.3f, 41.3f)), 91));

            // Zakazany las
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(15.6f, 10.1f), 1.5f), 92));
            // Boisko do quidditcha
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(15.2f, 45.4f), 1.8f), 93, isQuidditchPitch: true));
            // Eliksir obok zakazanego lasu
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(11.4f, 39.6f), 1.5f), 94));
            // Chatka Hagrida
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(10.7f, 43.1f), 1.7f), 95));
            // Smocza Arena
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(5.9f, 45.0f), 2.2f), 96));
            // Sprawowanie obok opieki nad magicznymi stworzeniami
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(9.7f, 10.8f), 1.1f), 97));
            // Opieka nad magicznymi stworzeniami
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(6.7f, 37.3f), 1.6f), 98));
            // +4PŹ niedaleko Jeziora
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(8.3f, 32.3f), 1.3f), 99));
            // +5PŹ obok Jeziora
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(6.5f, 28.2f), 1.3f), 100));
            // Jezioro
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.6f, 28.9f), new Vector2(16.4f, 29.8f), new Vector2(17.4f, 25.3f), new Vector2(8.7f, 25.3f)), 101));
        }

        private void InitSecondBoard()
        {
            int boardId = 1;

            // Dziurawy Kocioł
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(19.3f, 14.6f), 2.0f), 102));
            // Sklep z kotłami Madame Potage
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(12.9f, 8.4f), 1.5f), 103));
            // Markowy sprzęt do quidditcha
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(17.5f, 18.0f), 1.6f), 104));
            // Esy i floresy
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(14.0f, 20.4f), 2.0f), 105));
            // Bank Gringotta
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(8.9f, 20.7f), 2.2f), 106));
            // Zaklęcie na ulicy śmiertelnego Nokturnu obok +3PŻ
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(12.2f, 13.1f), 1.3f), 107));
            // +3PŻ na ulicy śmiertelnego Nokturnu
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(8.9f, 13.0f), 1.5f), 108));
            // +2PŻ na ulicy śmiertelnego Nokturnu
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(11.4f, 8.0f), 1.6f), 109));
            // Zaklęcie na ulicy śmiertelnego Nokturnu obok +2PŻ
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(15.6f, 6.3f), 1.5f), 110));
            // Borgin & Burkes
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(18.3f, 3.8f), 2.0f), 111));
            // Magiczna Menażeria
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(4.1f, 18.1f), 1.8f), 112));
            // Czarodziejskie niespodzianki Gambola i Japesa
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(3.4f, 13.0f), 2.0f), 113));
            // Zaklęcie obok czarodziejskich niespodzianek Gambola i Japesa
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(2.0f, 9.4f), 1.3f), 114));
            // Księga obok zaklęcia obok czarodziejskich niespodzianek Gambola i Japesa
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(9.1f, 9.9f), 1.3f), 115));
            // Eliksir obok sklepu Ollivandera
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(9.2f, 3.6f), 1.3f), 116));
            // Sklep Ollivandera
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(4.7f, 5.2f), 2.0f), 117));
        }

        private void InitThirdBoard()
        {
            int boardId = 2;

            // Miodowe Królestwo
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(23.0f, 3.5f), 1.3f), 118));
            // Sklep Zonka
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(3.8f, 18.7f), 1.3f), 119));
            // Wrzeszcząca Chata
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(31.3f, 6.6f), 1.4f), 120));
            // Pod świńskim łbem
            boards[boardId].Fields.Add(new Field(boardId, new Circle(new Vector2(17.7f, 7.3f), 1.3f), 121));

            // Bank Gringotta
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.1f, 8.3f), new Vector2(16.0f, 8.3f), new Vector2(16.0f, 1.4f), new Vector2(9.1f, 1.4f)), 122));
            // Ministerstwo Magii
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(1.1f, 8.3f), new Vector2(8.0f, 8.3f), new Vector2(8.0f, 1.4f), new Vector2(1.1f, 1.4f)), 123));
            // Grimmauld Place 12
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(17.2f, 16.3f), new Vector2(24.1f, 16.3f), new Vector2(24.1f, 9.4f), new Vector2(17.2f, 9.4f)), 124));
            // Nora
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.1f, 16.3f), new Vector2(16.0f, 16.3f), new Vector2(16.0f, 9.4f), new Vector2(9.1f, 9.4f)), 125));
            // Sala przepowiedni
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(1.1f, 16.3f), new Vector2(8.0f, 16.3f), new Vector2(8.0f, 9.4f), new Vector2(1.1f, 9.4f)), 126));
            // Forest of dean
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(17.2f, 24.2f), new Vector2(24.1f, 24.2f), new Vector2(24.1f, 17.3f), new Vector2(17.2f, 17.3f)), 127));
            // Dwór Malfoya
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(9.1f, 24.2f), new Vector2(16.0f, 24.2f), new Vector2(16.0f, 17.3f), new Vector2(9.1f, 17.3f)), 128));
            // Cmentarz w Little Hangleton
            boards[boardId].Fields.Add(new Field(boardId, new Quadrangle(
                new Vector2(1.1f, 24.2f), new Vector2(8.0f, 24.2f), new Vector2(8.0f, 17.3f), new Vector2(1.1f, 17.3f)), 129));
        }

        private void AddNeighborsToFieldOnFirstBoard()
        {
            int boardId = 0;

            boards[boardId].GetFieldById(0).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 1 }));
            boards[boardId].GetFieldById(1).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 0, 2, 102 }));
            boards[boardId].GetFieldById(2).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 1, 3 }));
            boards[boardId].GetFieldById(3).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 2, 4, 6, 21 }));
            boards[boardId].GetFieldById(4).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 3, 5 }));
            boards[boardId].GetFieldById(5).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 4 }));
            boards[boardId].GetFieldById(6).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 3, 7 }));
            boards[boardId].GetFieldById(7).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 6, 8 }));
            boards[boardId].GetFieldById(8).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 7, 9 }));
            boards[boardId].GetFieldById(9).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 8, 10 }));
            boards[boardId].GetFieldById(10).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 9, 11, 78 }));
            boards[boardId].GetFieldById(11).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 10, 12 }));
            boards[boardId].GetFieldById(12).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 11, 13 }));
            boards[boardId].GetFieldById(13).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 12, 14 }));
            boards[boardId].GetFieldById(14).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 13, 15 }));
            boards[boardId].GetFieldById(15).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 14, 16, 17, 22 }));
            boards[boardId].GetFieldById(16).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 15 }));
            boards[boardId].GetFieldById(17).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 15, 18 }));
            boards[boardId].GetFieldById(18).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 17, 19 }));
            boards[boardId].GetFieldById(19).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 18, 20 }));
            boards[boardId].GetFieldById(20).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 19, 21 }));
            boards[boardId].GetFieldById(21).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 3, 20 }));
            boards[boardId].GetFieldById(22).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 15, 23 }));
            boards[boardId].GetFieldById(23).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 22, 24, 36 }));
            boards[boardId].GetFieldById(24).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 23, 25 }));
            boards[boardId].GetFieldById(25).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 24, 26 }));
            boards[boardId].GetFieldById(26).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 25, 27, 37 }));
            boards[boardId].GetFieldById(27).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 26, 28, 37, 38 }));
            boards[boardId].GetFieldById(28).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 27, 29 }));
            boards[boardId].GetFieldById(29).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 28, 30 }));
            boards[boardId].GetFieldById(30).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 29, 31 }));
            boards[boardId].GetFieldById(31).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 30, 32 }));
            boards[boardId].GetFieldById(32).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 31, 33, 37 }));
            boards[boardId].GetFieldById(33).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 32, 34, 37 }));
            boards[boardId].GetFieldById(34).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 33, 35, 37 }));
            boards[boardId].GetFieldById(35).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 34, 36 }));
            boards[boardId].GetFieldById(36).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 35, 23 }));
            boards[boardId].GetFieldById(37).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 26, 27, 32, 33, 34 }));
            boards[boardId].GetFieldById(38).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 27, 39 }));
            boards[boardId].GetFieldById(39).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 38, 40, 52, 53 }));
            boards[boardId].GetFieldById(40).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 39, 41, 53 }));
            boards[boardId].GetFieldById(41).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 40, 42 }));
            boards[boardId].GetFieldById(42).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 41, 43 }));
            boards[boardId].GetFieldById(43).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 42, 44 }));
            boards[boardId].GetFieldById(44).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 43, 45 }));
            boards[boardId].GetFieldById(45).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 44, 46, 53, 55 }));
            boards[boardId].GetFieldById(46).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 45, 47, 53 }));
            boards[boardId].GetFieldById(47).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 46, 48 }));
            boards[boardId].GetFieldById(48).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 47, 49 }));
            boards[boardId].GetFieldById(49).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 48, 50, 54 }));
            boards[boardId].GetFieldById(50).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 49, 51 }));
            boards[boardId].GetFieldById(51).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 50, 52 }));
            boards[boardId].GetFieldById(52).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 39, 51, 53 }));
            boards[boardId].GetFieldById(53).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 39, 40, 45, 46, 52 }));
            boards[boardId].GetFieldById(54).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 11, 49 }));
            boards[boardId].GetFieldById(55).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 45, 56 }));
            boards[boardId].GetFieldById(56).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 55, 57, 73 }));
            boards[boardId].GetFieldById(57).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 56, 58 }));
            boards[boardId].GetFieldById(58).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 57, 59 }));
            boards[boardId].GetFieldById(59).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 58, 60 }));
            boards[boardId].GetFieldById(60).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 59, 61 }));
            boards[boardId].GetFieldById(61).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 60, 62 }));
            boards[boardId].GetFieldById(62).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 61, 63, 67 }));
            boards[boardId].GetFieldById(63).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 62, 64 }));
            boards[boardId].GetFieldById(64).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 63, 65 }));
            boards[boardId].GetFieldById(65).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 64, 66 }));
            boards[boardId].GetFieldById(66).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 65 }));
            boards[boardId].GetFieldById(67).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 62, 68 }));
            boards[boardId].GetFieldById(68).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 67, 69, 70, 91 }));
            boards[boardId].GetFieldById(69).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 68 }));
            boards[boardId].GetFieldById(70).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 68, 71, 85 }));
            boards[boardId].GetFieldById(71).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 70, 72 }));
            boards[boardId].GetFieldById(72).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 71, 73, 74 }));
            boards[boardId].GetFieldById(73).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 72, 56 }));
            boards[boardId].GetFieldById(74).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 72, 75 }));
            boards[boardId].GetFieldById(75).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 74, 76 }));
            boards[boardId].GetFieldById(76).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 75, 77 }));
            boards[boardId].GetFieldById(77).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 76, 78, 79 }));
            boards[boardId].GetFieldById(78).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 10, 77 }));
            boards[boardId].GetFieldById(79).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 77, 80, 81, 82 }));
            boards[boardId].GetFieldById(80).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 79 }));
            boards[boardId].GetFieldById(81).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 79 }));
            boards[boardId].GetFieldById(82).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 79, 83, 86 }));
            boards[boardId].GetFieldById(83).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 82, 84 }));
            boards[boardId].GetFieldById(84).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 83, 85 }));
            boards[boardId].GetFieldById(85).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 84, 70 }));
            boards[boardId].GetFieldById(86).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 82, 87, 92 }));
            boards[boardId].GetFieldById(87).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 86, 88, 89 }));
            boards[boardId].GetFieldById(88).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 87 }));
            boards[boardId].GetFieldById(89).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 87, 90 }));
            boards[boardId].GetFieldById(90).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 89, 91 }));
            boards[boardId].GetFieldById(91).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 68, 90 }));
            boards[boardId].GetFieldById(92).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 86, 93, 94 }));
            boards[boardId].GetFieldById(93).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 92 }));
            boards[boardId].GetFieldById(94).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 92, 95, 96, 97 }));
            boards[boardId].GetFieldById(95).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 94, 96 }));
            boards[boardId].GetFieldById(96).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 95, 94 }));
            boards[boardId].GetFieldById(97).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 94, 98, 99 }));
            boards[boardId].GetFieldById(98).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 97 }));
            boards[boardId].GetFieldById(99).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 97, 100 }));
            boards[boardId].GetFieldById(100).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 99, 101, 118 }));
            boards[boardId].GetFieldById(101).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 100 }));
        }

        private void AddNeighborsToFieldOnSecondBoard()
        {
            int boardId = 1;

            boards[boardId].GetFieldById(102).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 1, 103 }));
            boards[boardId].GetFieldById(103).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 102, 104, 105, 106, 107, 112, 113, 114 }));
            boards[boardId].GetFieldById(104).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 105, 106, 107, 112, 113, 114 }));
            boards[boardId].GetFieldById(105).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 104, 106, 107, 112, 113, 114 }));
            boards[boardId].GetFieldById(106).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 104, 105, 107, 112, 113, 114 }));
            boards[boardId].GetFieldById(107).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 104, 105, 106, 108, 109, 112, 113, 114 }));
            boards[boardId].GetFieldById(108).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 107 }));
            boards[boardId].GetFieldById(109).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 107, 110 }));
            boards[boardId].GetFieldById(110).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 109, 111 }));
            boards[boardId].GetFieldById(111).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 110 }));
            boards[boardId].GetFieldById(112).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 104, 105, 106, 107, 113, 114 }));
            boards[boardId].GetFieldById(113).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 104, 105, 106, 107, 112, 114 }));
            boards[boardId].GetFieldById(114).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 103, 104, 105, 106, 107, 112, 113, 115 }));
            boards[boardId].GetFieldById(115).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 114, 116 }));
            boards[boardId].GetFieldById(116).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 115, 117 }));
            boards[boardId].GetFieldById(117).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 116 }));
        }

        private void AddNeighborsToFieldOnThirdBoard()
        {
            int boardId = 2;

            boards[boardId].GetFieldById(118).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 100, 119, 120, 121 }));
            boards[boardId].GetFieldById(119).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 118, 120, 121 }));
            boards[boardId].GetFieldById(120).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 118, 119, 121 }));
            boards[boardId].GetFieldById(121).AddNeighbors(boards[boardId].GetFieldsByIds(new List<int>() { 118, 119, 120 }));
        }

        private void AddPortalFieldsToFieldOnFirstBoard()
        {
            int boardId = 0;

            boards[boardId].GetFieldById(5).SetPortalField(boards[boardId].GetFieldById(118));
            boards[boardId].GetFieldById(69).SetPortalField(boards[boardId].GetFieldById(120));
            boards[boardId].GetFieldById(81).SetPortalField(boards[boardId].GetFieldById(121));
        }

        private void AddPortalFieldsToFieldOnSecondBoard()
        {
            int boardId = 1;

            boards[boardId].GetFieldById(106).SetPortalField(boards[boardId].GetFieldById(122));
        }

        private void AddPortalFieldsToFieldOnThirdBoard()
        {
            int boardId = 2;

            boards[boardId].GetFieldById(118).SetPortalField(boards[boardId].GetFieldById(5));
            boards[boardId].GetFieldById(120).SetPortalField(boards[boardId].GetFieldById(69));
            boards[boardId].GetFieldById(121).SetPortalField(boards[boardId].GetFieldById(81));
            boards[boardId].GetFieldById(122).SetPortalField(boards[boardId].GetFieldById(106));
            boards[boardId].GetFieldById(123).SetPortalField(boards[boardId].GetFieldById(126));
            boards[boardId].GetFieldById(126).SetPortalField(boards[boardId].GetFieldById(123));
        }
    }
}
