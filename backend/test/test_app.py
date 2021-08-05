import unittest
from unittest import TestCase
from app import create_app


class TestIntegrations(TestCase):
    def setUp(self):
        app = create_app({'TESTING': True})
        self.client = app.test_client()

    def test_run_circuit(self):
        with open('circuits/circuit_ok.qasm', 'r') as file:
            qasm_code = file.read().replace('\n', '')
        response = self.client.post('/circuit', data=qasm_code)

        self.assertEqual(200, response.status_code)
        self.assertEqual(2, len(response.get_json()))
        self.assertIn("00", response.get_json())
        self.assertIn("01", response.get_json())

    def test_run_corrupt_circuit(self):
        with open('circuits/circuit_corrupt.qasm', 'r') as file:
            qasm_code = file.read().replace('\n', '')
        response = self.client.post('/circuit', data=qasm_code)
        self.assertEqual(400, response.status_code)


if __name__ == "__main__":
    unittest.main()