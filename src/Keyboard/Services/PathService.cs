using System;
using System.Linq;
using System.Text;

namespace Keyboard.Services
{
    public class PathService : IPathService
    {
        private int _currentCursorPosition;
        private int _currentRow;
        private int _currentColumn;
        private const string UP = "U";
        private const string DOWN = "D";
        private const string LEFT = "L";
        private const string RIGHT = "R";
        private const string SPACE = "S";
        private const string SELECT = "#";

        private readonly char[] KEYBOARD_CHARACTERS = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private const int CHARACTERS_PER_ROW = 6;
        private StringBuilder _sb;

        public PathService()
        {
            _sb = new StringBuilder();
        }
        
        public string GetPath(string topic)
        {
            _reset();

            if (topic.Length == 0)
            {
                return string.Empty;
            }

            topic = topic.ToUpper();

            foreach (var character in topic.ToCharArray())
            {
                _tracePathToCharacter(character);
            }

            return string.Join(",", _sb.ToString().ToCharArray());
        }

        private void _reset()
        {
            _currentCursorPosition = 0;
            _currentRow = 0;
            _currentColumn = 0;
            _sb.Clear();
        }

        private void _tracePathToCharacter(char characterToFind)
        {
            if (characterToFind == KEYBOARD_CHARACTERS[_currentCursorPosition])
            {
                //we've found it
                _sb.Append(SELECT);
                return;
            }
            else if (characterToFind == ' ')
            {
                _sb.Append(SPACE);
                
                return;
            }
            else
            {
                var coordinatesOfCharacterToFind = _getCharacterCoordinates(characterToFind);

                //am I on the right row?
                if (coordinatesOfCharacterToFind.Row == _currentRow)
                {
                    //go left or right
                    if (coordinatesOfCharacterToFind.Column > _currentColumn)
                    {
                        _currentCursorPosition++;
                        _currentColumn++;
                        _sb.Append(RIGHT);
                    }
                    else
                    {
                        _currentCursorPosition--;
                        _currentColumn--;
                        _sb.Append(LEFT);
                    }

                    _tracePathToCharacter(characterToFind);
                }
                else
                {
                    //go up or down
                    if (coordinatesOfCharacterToFind.Row > _currentRow)
                    {
                        _currentRow++;
                        _currentCursorPosition += CHARACTERS_PER_ROW;
                        _sb.Append(DOWN);
                    }
                    else
                    {
                        _currentRow--;
                        _currentCursorPosition -= CHARACTERS_PER_ROW;
                        _sb.Append(UP);
                    }

                    _tracePathToCharacter(characterToFind);
                }
            }
        }

        private CharacterCoordinates _getCharacterCoordinates(char character)
        {
            var indexOfCharacter = Array.FindIndex(KEYBOARD_CHARACTERS, c => c == character);

            var column = indexOfCharacter % CHARACTERS_PER_ROW;
            var row = indexOfCharacter / CHARACTERS_PER_ROW;

            return new CharacterCoordinates
            {
                Column = column,
                Row = row
            };
        }

        private class CharacterCoordinates
        {
            public int Column;
            public int Row;
        }
    }
}
