from qiskit import QuantumCircuit, transpile, assemble
from qiskit.providers.aer import Aer
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
    # with open('test/circuits/circuit_ok.qasm', 'r') as file:
    #     qasm_str = file.read().replace('\n', '')
    #
    # circ = QuantumCircuit.from_qasm_str(qasm_str)
    # circ.save_statevector()
    #
    # simulator = Aer.get_backend('aer_simulator')
    #
    # qobj = assemble(circ)  # Create a Qobj from the circuit for the simulator to run
    # result = simulator.run(qobj).result()
    #
    # out_state = result.get_statevector()
    # print(out_state)  # Display the output state vector
    circ = QuantumCircuit(2)
    circ.h(0)
    circ.x(1)
    circ.save_statevector()
    circ.cx(0, 1)
    circ.save_statevector(label="state1")
    circ.h(0)
    circ.save_statevector(label="state2")
    # circ.measure_active()

    # circ.draw("mpl", filename="image")
    print(*circ.data, sep="\n")


    # Transpile for simulator
    simulator = Aer.get_backend('aer_simulator')
    circ = transpile(circ, simulator)

    circ.draw("mpl", filename="image")

    # Run and get statevector
    result = simulator.run(circ).result()
    print(result)
    # statevector = result.get_statevector()
    # print(statevector)
    # plot_state_city(statevector, title='Bell state')



if __name__ == '__main__':
    # main()
    statevector()
