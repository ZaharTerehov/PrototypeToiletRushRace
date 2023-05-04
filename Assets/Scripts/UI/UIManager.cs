
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIManager : MonoBehaviour
	{
		private static UIManager _instance;

		[Header("Windows")]
		[SerializeField] private Main _main;
		[SerializeField] private LevelSelection _levelSelection;

		[Space]
		[SerializeField] private List<Window> _windows = new List<Window>();

		[Space]
		[SerializeField] private Image _fadePanelPopup;

		private Window _currentWindow;
		private Window _previousWindow;
		
		private Type _currentTypeWindow;

		public static Main MainWindow => _instance._main;
		public static LevelSelection LevelSelection => _instance._levelSelection;

		private readonly Dictionary<Type, Window> _windowsDictionary = new Dictionary<Type, Window>();

		private void Awake()
		{
			if (_instance == null)
				_instance = this;
			else
				Destroy(gameObject);
			
			FillDictionary(_windowsDictionary, _windows);
		}

		private void Start()
		{
			Open<Main>();
		}
		
		private void FillDictionary<T>(Dictionary<Type, T> dictionary, List<T> list)
		{
			foreach (var element in list)
			{
				dictionary.Add(element.GetType(), element);
			}
		}

		private async UniTask CloseCurrent(Type typeWindow)
		{
			if (typeWindow.IsSubclassOf(typeof(Window)))
			{
				if (_currentWindow != null)
				{
					_previousWindow = _currentWindow;
					_currentWindow.Hide();
				}
			}
		}

		public static async void Open<T>()
		{
			if (_instance._currentTypeWindow != null)
			{
				await _instance.CloseCurrent(_instance._currentTypeWindow);
				// _instance._fadePanelPopup.gameObject.SetActive(false);
			}

			_instance._currentTypeWindow= typeof(T);

			if (typeof(T).IsSubclassOf(typeof(Window)))
			{
				var window = _instance._windowsDictionary[typeof(T)];

				_instance._currentWindow = window;

				_instance._currentWindow.Show();
			}
		}
	}
}