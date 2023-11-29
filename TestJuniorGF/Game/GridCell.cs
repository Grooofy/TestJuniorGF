using System;
using System.Collections.Generic;

namespace TestJuniorGF
{
    public class GridCell
    {
        private readonly Random _random = new Random();
        private readonly ICell[,] _figures;
        private readonly int _gridSize;

        public GridCell(int gridSize)
        {
            _gridSize = gridSize;
            _figures = new ICell[_gridSize, _gridSize];
            RandomFill();
        }

        public ICell GetFigure(Vector2 position) => _figures[position.X, position.Y];

        public void SwapFigures(Vector2 firstPosition, Vector2 secondPosition) =>
            SwapFigures(firstPosition.X, firstPosition.Y, secondPosition.X, secondPosition.Y);

        public bool TryMatchAll()
        {
            bool isMatched = false;
            
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    if (ExecuteMatch(new Vector2(i, j), ref _figures[i, j]))
                    {
                        isMatched = true;
                    }
                }
            }
            return isMatched;
        }

        public bool TryMatch(Vector2 firstPosition, Vector2 secondPosition)
        {
            var firstTry = ExecuteMatch(secondPosition, ref _figures[secondPosition.X, secondPosition.Y]);
            var secondTry = ExecuteMatch(firstPosition, ref _figures[firstPosition.X, firstPosition.Y]);
            return (firstTry || secondTry);
        }

        public void PushFiguresDown(out List<Vector2> dropsFrom, out List<Vector2> dropsTo)
        {
            dropsFrom = new List<Vector2>();
            dropsTo = new List<Vector2>();
            
            for (int i = 0; i < _gridSize; i++)
            {
                int gap = 0;
                
                for (int j = _gridSize - 1; j >= 0; j--)
                {
                    if (_figures[i, j].IsNullObject)
                    {
                        gap++;
                    }
                    else
                    {
                        SwapFigures(i, j + gap, i, j);
                        dropsFrom.Add(new Vector2(i, j));
                        dropsTo.Add(new Vector2(i, j + gap));
                    }
                }
            }
        }

        public void RandomFill()
        {
            var figureTypes = Enum.GetValues(typeof(CellColor));
            
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    if (_figures[i, j] == null || _figures[i, j].IsNullObject)
                    {
                        var randomType = (CellColor)figureTypes.GetValue(_random.Next(figureTypes.Length));
                        _figures[i, j] = new Default(randomType, new Vector2(i, j));
                    }
                }
            }
        }

        private void SwapFigures(int x1, int y1, int x2, int y2)
        {
            (_figures[x1, y1].Position, _figures[x2, y2].Position) = (_figures[x2, y2].Position, _figures[x1, y1].Position);
            
            (_figures[x1, y1], _figures[x2, y2]) = (_figures[x2, y2], _figures[x1, y1]);
        }

        private bool ExecuteMatch(Vector2 position, ref ICell firstFigure)
        {
            var matchList = GetMatchList(position, firstFigure.Color);

            if (matchList.Count == 0)
                return false;

            if (Game.IsInitialized && !TrySetBonus(matchList, ref firstFigure))
            {
                matchList.Add(firstFigure);
            }

            foreach (var figure in matchList)
            {
                figure.Destroy(_figures);
            }
            return true;
        }

        private bool TrySetBonus(List<ICell> match, ref ICell cellToSet)
        {
            bool isEnoughForBomb = match.Count >= 4;
            bool isEnoughForLine = match.Count == 3;
            
            if (isEnoughForBomb)
            {
                cellToSet = new Bomb(cellToSet);
                return true;
            }

            if (isEnoughForLine)
            {
                if (match[0].Position.X == cellToSet.Position.X)
                {
                    cellToSet = new VerticalLine(cellToSet);
                }
                else
                {
                    cellToSet = new HorizontalLine(cellToSet);
                }
                return true;
            }
            return false;
        }
      
        private List<ICell> GetMatchList(Vector2 position, CellColor _color)
        {
            int horCounter = position.X + 1;
            int vertCounter = position.Y + 1;
            var verticalLine = new List<ICell>();
            var horizontalLine = new List<ICell>();
            
            while (horCounter < _gridSize)
            {
                if (_figures[horCounter, position.Y].Color != _color)
                    break;
                horizontalLine.Add(_figures[horCounter, position.Y]);
                horCounter++;
            }

            while (vertCounter < _gridSize)
            {
                if (_figures[position.X, vertCounter].Color != _color)
                    break;
                verticalLine.Add(_figures[position.X, vertCounter]);
                vertCounter++;
            }

            horCounter = position.X - 1;
            vertCounter = position.Y - 1;
            
            while (horCounter >= 0)
            {
                if (_figures[horCounter, position.Y].Color != _color)
                    break;
                horizontalLine.Add(_figures[horCounter, position.Y]);
                horCounter--;
            }

            while (vertCounter >= 0)
            {
                if (_figures[position.X, vertCounter].Color != _color)
                    break;
                verticalLine.Add(_figures[position.X, vertCounter]);
                vertCounter--;
            }

            if (verticalLine.Count < 2)
            {
                verticalLine.Clear();
            }

            if (horizontalLine.Count < 2)
            {
                horizontalLine.Clear();
            }

            verticalLine.AddRange(horizontalLine);
            return verticalLine;
        }
    }
}
