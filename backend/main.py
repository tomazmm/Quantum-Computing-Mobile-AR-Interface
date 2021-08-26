import random
import sys

from qiskit import QuantumCircuit, transpile, assemble
from qiskit.circuit import Measure
from qiskit.providers.aer import Aer
from qiskit.providers.aer.library import SaveStatevector
from qiskit.providers.aer.library.default_qubits import default_qubits
from qiskit.visualization import plot_state_city
from pprint import pprint


def main():
    # Construct quantum circuit without measure
    circ = QuantumCircuit(2)
    circ.h(0)
    circ.cx(0, 1)
    circ.draw("text")

    with open('test/circuits/circuit_ok.qasm', 'r') as file:
        qasm_str = file.read().replace('\n', '')

    circ = QuantumCircuit.from_qasm_str(qasm_str)
    circ.save_unitary()

    # Transpile for simulator
    simulator = Aer.get_backend('aer_simulator')
    circ = transpile(circ, simulator)

    # Run and get unitary
    result = simulator.run(circ).result()
    unitary = result.get_unitary(circ)
    print("Circuit unitary:\n", unitary.round(5))


def statevector():
    with open('test/circuits/Multi7x1Mod15.qasm', 'r') as file:
        qasm_str = file.read().replace('\n', '')
    circ = QuantumCircuit.from_qasm_str(qasm_str)

    qubits = default_qubits(circ)
    try:
        data_copy = circ.data.copy()
        i = 0
        while data_copy:
            circuit_element = data_copy[i]
            if type(circuit_element[0]) is not SaveStatevector:
                sv = SaveStatevector(len(qubits), label=f"sv-{i}")
                data_copy.insert(i + 1, (sv, qubits, []))
            i += 1
    except IndexError:
        circ.data = data_copy

    # Transpile for simulator
    simulator = Aer.get_backend('aer_simulator')
    circ = transpile(circ, simulator)

    circ.draw("mpl", filename="image")

    # Run and get statevector
    result = simulator.run(circ).result()
    pprint(result.data())


def shor():
    circ = QuantumCircuit(4)
    circ.h(0)
    circ.h(1)
    circ.h(2)
    circ.h(3)
    circ.save_statevector()

    circ.x(0)
    circ.x(1)
    circ.x(2)
    circ.x(3)
    circ.save_statevector(label="second")

    circ.x(0)
    circ.cx(1, 2)
    circ.save_statevector(label="third")

    print(*circ.data, sep="\n")

    # Transpile for simulator
    simulator = Aer.get_backend('aer_simulator')
    circ = transpile(circ, simulator)

    circ.draw("mpl", filename="image")

    # Run and get statevector
    result = simulator.run(circ).result()
    print(result)


if __name__ == '__main__':
    # main()
    statevector()
